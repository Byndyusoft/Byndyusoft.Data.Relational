namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     A base class for an database repository.
    /// </summary>
    public abstract class DbRepositoryBase
    {
        private readonly IDbSessionAccessor _sessionAccessor;

        protected DbRepositoryBase(IDbSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        protected IDbSession Session => _sessionAccessor.DbSession;
    }
}