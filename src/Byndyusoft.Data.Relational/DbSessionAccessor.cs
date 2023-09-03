using Byndyusoft.Data.Relational.Sessions;
using Byndyusoft.Data.Sessions;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        private readonly IDbSessionFactory _sessionFactory;
        private readonly IDbSessionStorage _sessionStorage;
        private readonly ISessionAccessor? _sessionAccessor;
        private DbSessionsIndexer? _indexer;

        public DbSessionAccessor(
            IDbSessionFactory sessionFactory, 
            IDbSessionStorage sessionStorage,
            ISessionAccessor? sessionAccessor = null)
        {
            _sessionFactory = sessionFactory;
            _sessionStorage = sessionStorage;
            _sessionAccessor = sessionAccessor;
        }

        public IDbSession? DbSession => DbSessions[Options.DefaultName];

        public IDbSessionsIndexer DbSessions
        {
            get
            {
                var session = _sessionAccessor?.Session;
                if (session is not null)
                    return _indexer ??= new DbSessionsIndexer(_sessionFactory, _sessionAccessor!);
                return _sessionStorage;
            }
        }
    }
}