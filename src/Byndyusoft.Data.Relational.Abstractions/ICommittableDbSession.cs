using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Represents a commitable database session.
    /// </summary>
    public interface ICommittableDbSession : IDbSession
    {
        /// <summary>
        ///     Gets the isolation level for this <see cref="ICommittableDbSession" />.
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        ///     Asynchronously commits the <see cref="ICommittableDbSession" />.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Asynchronously rolls back the <see cref="ICommittableDbSession" />.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}