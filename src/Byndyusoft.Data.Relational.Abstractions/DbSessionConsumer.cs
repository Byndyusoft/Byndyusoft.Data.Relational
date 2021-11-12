namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Abstract class that consumes <see cref="IDbSession" />.
    /// </summary>
    public abstract class DbSessionConsumer
    {
        private readonly IDbSessionAccessor _sessionAccessor;

        /// <summary>
        ///     Initializes a new instance of <see cref="DbSessionConsumer" /> with specified <paramref name="sessionAccessor" />.
        /// </summary>
        /// <param name="sessionAccessor">An instance of <see cref="IDbSessionAccessor" />.</param>
        protected DbSessionConsumer(IDbSessionAccessor sessionAccessor)
        {
            _sessionAccessor = sessionAccessor;
        }

        /// <summary>
        ///     Gets the current <see cref="IDbSession" />.
        /// </summary>
        protected IDbSession? DbSession => _sessionAccessor.DbSession;
    }
}