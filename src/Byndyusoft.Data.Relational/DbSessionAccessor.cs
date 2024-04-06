using Byndyusoft.Data.Relational.Sessions;
using Byndyusoft.Data.Sessions;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor, IDbSessionsIndexer
    {
        private readonly IDbSessionFactory _sessionFactory;
        private readonly IDbSessionStorage _sessionStorage;
        private readonly ISessionAccessor? _sessionAccessor;

        public DbSessionAccessor(
            IDbSessionFactory sessionFactory, 
            IDbSessionStorage sessionStorage,
            ISessionAccessor? sessionAccessor = null)
        {
            Guard.IsNotNull(sessionFactory, nameof(sessionFactory));
            Guard.IsNotNull(sessionStorage, nameof(sessionStorage));

            _sessionFactory = sessionFactory;
            _sessionStorage = sessionStorage;
            _sessionAccessor = sessionAccessor;
        }

        public IDbSession? DbSession => DbSessions[Options.DefaultName];

        public IDbSessionsIndexer DbSessions => this;

        IDbSession? IDbSessionsIndexer.this[string name]
        {
            get
            {
                Guard.IsNotNull(name, nameof(name));

                var session = _sessionAccessor?.Session;
                if (session is null)
                {
                    return _sessionStorage.GetCurrent(name);
                }

                var key = $"Byndyusoft.Data.Relational.{name}";
                var dbSession = session.GetOrEnlist(key, () => Create(name, session));
                return dbSession.DbSession;
            }
        }

        private IDependentDbSession Create(string name, ISession session)
        {
            if (session is ICommittableSession committableSession)
            {
                var dbSession = _sessionFactory.CreateCommittableSessionAsync(name, committableSession.IsolationLevel)
                    .ConfigureAwait(false).GetAwaiter().GetResult();
                return new DependentCommittableDbSession(dbSession);
            }
            else
            {
                var dbSession = _sessionFactory.CreateSessionAsync(name)
                    .ConfigureAwait(false).GetAwaiter().GetResult();
                return new DependentDbSession(dbSession);
            }
        }
    }
}