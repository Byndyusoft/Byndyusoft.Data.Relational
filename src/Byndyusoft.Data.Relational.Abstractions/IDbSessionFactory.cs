using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Represents a set of methods for creating instances of database sessions.
    /// </summary>
    public interface IDbSessionFactory
    {
        /// <summary>
        ///     Returns a new instance of the database session's class that implements the <see cref="IDbSession" /> interface.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="IDbSession" />.</returns>
        Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Returns a new instance of the database session's class that implements the <see cref="IDbSession" /> interface with
        ///     specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IDbSession" />.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="IDbSession" />.</returns>
        Task<IDbSession> CreateSessionAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Returns a new instance of the commitable database session's class that implements the
        ///     <see cref="ICommittableDbSession" /> interface.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="ICommittableDbSession" />.</returns>
        Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Returns a new instance of the commitable database session's class that implements the
        ///     <see cref="ICommittableDbSession" /> interface with specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IDbSession" />.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="ICommittableDbSession" />.</returns>
        Task<ICommittableDbSession> CreateCommittableSessionAsync(string name,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Returns a new instance of the commitable database session's class that implements the
        ///     <see cref="ICommittableDbSession" /> interface with specified <see cref="IsolationLevel" />.
        /// </summary>
        /// <param name="isolationLevel">
        ///     One of the enumeration values that specifies the isolation level for the transaction to
        ///     use.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="ICommittableDbSession" />.</returns>
        Task<ICommittableDbSession> CreateCommittableSessionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Returns a new instance of the commitable database session's class that implements the
        ///     <see cref="ICommittableDbSession" /> interface with specified name and <see cref="IsolationLevel" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="IDbSession" />.</param>
        /// <param name="isolationLevel">
        ///     One of the enumeration values that specifies the isolation level for the transaction to
        ///     use.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A new instance of <see cref="ICommittableDbSession" />.</returns>
        Task<ICommittableDbSession> CreateCommittableSessionAsync(string name, IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default);
    }
}