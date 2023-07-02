using Byndyusoft.Data.Sessions;

namespace Byndyusoft.Data.Relational.Sessions
{
    public class DbSessionsIndexer : IDbSessionsIndexer
    {
        private readonly IDbSessionFactory _sessionFactory;
        private readonly ISessionAccessor _sessionAccessor;

        public DbSessionsIndexer(
            IDbSessionFactory sessionFactory,
            ISessionAccessor sessionAccessor)
        {
            _sessionFactory = sessionFactory;
            _sessionAccessor = sessionAccessor;
        }

        public IDbSession? this[string name]
        {
            get
            {
                var key = $"Byndyusoft.Data.Relational.{name}";

                var session = _sessionAccessor.Session;
                var dbSession =  session?.GetOrEnlist(key, () => Create(name, session));
                return dbSession?.DbSession;
            }
        }

        private IDependentDbSession Create(string name, ISession session)
        {
            if (session is ICommitableSession committableSession)
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