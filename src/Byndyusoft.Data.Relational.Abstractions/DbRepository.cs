namespace Byndyusoft.Data.Relational
{
    public abstract class DbRepository
    {
        private readonly IDbSessionAccessor _sessionAccessor;

        protected DbRepository(IDbSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        protected IDbSession Session => _sessionAccessor.DbSession;
    }
}