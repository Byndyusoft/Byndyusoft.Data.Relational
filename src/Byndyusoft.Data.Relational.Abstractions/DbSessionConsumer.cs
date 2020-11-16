namespace Byndyusoft.Data.Relational
{
    public abstract class DbSessionConsumer
    {
        private readonly IDbSessionAccessor _sessionAccessor;

        protected DbSessionConsumer(IDbSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        protected IDbSession DbSession => _sessionAccessor.DbSession;
    }
}
