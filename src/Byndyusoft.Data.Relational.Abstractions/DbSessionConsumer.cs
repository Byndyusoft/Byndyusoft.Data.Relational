using System;
using CommunityToolkit.Diagnostics;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Abstract class that consumes <see cref="IDbSession" />.
    /// </summary>
    public abstract class DbSessionConsumer
    {
        private readonly IDbSessionAccessor _sessionAccessor;
        private SessionIndexer? _sessionIndexer;

        /// <summary>
        ///     Initializes a new instance of <see cref="DbSessionConsumer" /> with specified <paramref name="sessionAccessor" />.
        /// </summary>
        /// <param name="sessionAccessor">An instance of <see cref="IDbSessionAccessor" />.</param>
        protected DbSessionConsumer(IDbSessionAccessor sessionAccessor)
        {
            Guard.IsNotNull(sessionAccessor, nameof(sessionAccessor));

            _sessionAccessor = sessionAccessor;
        }

        /// <summary>
        ///     Gets the current <see cref="IDbSession" />.
        /// </summary>
        protected IDbSession DbSession =>  _sessionAccessor.DbSession ??
                                          throw new InvalidOperationException(
                                              $"There is no current {nameof(DbSession)}");

        /// <summary>
        ///     Gets the current <see cref="DbSessions" /> indexer.
        /// </summary>
        protected SessionIndexer DbSessions => _sessionIndexer ??= new SessionIndexer(_sessionAccessor);

        protected class SessionIndexer
        {
            private readonly IDbSessionAccessor _sessionAccessor;

            public SessionIndexer(IDbSessionAccessor sessionAccessor)
            {
                _sessionAccessor = sessionAccessor;
            }

            public IDbSession this[string name] =>
                _sessionAccessor.DbSessions[name] ??
                throw new InvalidOperationException(
                    $"There is no current {nameof(DbSession)} with name {name}");
        }
    }
}