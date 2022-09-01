using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        private readonly IDbSessionStorage _sessionStorage;

        public DbSessionAccessor(IDbSessionStorage? sessionStorage = null)
        {
            _sessionStorage = sessionStorage ?? new AsyncLocalDbSessionStorage();
        }

        public IDbSession? DbSession => DbSessions[Options.DefaultName];

        public IDbSessionsIndexer DbSessions => _sessionStorage;
    }
}