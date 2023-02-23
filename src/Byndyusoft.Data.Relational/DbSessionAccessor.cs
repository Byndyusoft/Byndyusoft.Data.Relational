using Byndyusoft.Data.Sessions;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        private readonly IDbSessionFactory _sessionFactory;
        private readonly ISessionStorage? _sessionStorage;
        private DbSessionsIndexer? _indexer;

        public DbSessionAccessor(IDbSessionFactory sessionFactory, ISessionStorage? sessionStorage = null)
        {
            _sessionFactory = sessionFactory;
            _sessionStorage = sessionStorage;
        }

        public IDbSession? DbSession => DbSessions[Options.DefaultName];

        public IDbSessionsIndexer DbSessions
        {
            get
            {
                var session = _sessionStorage?.GetCurrent();
                if (session is not null)
                    return _indexer ??= new DbSessionsIndexer(_sessionFactory, _sessionStorage!);
                return Relational.DbSession.Current;
            }
        }
    }
}