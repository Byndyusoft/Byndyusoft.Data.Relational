using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Represents a database session - object that aggregates connection and transaction.
    /// </summary>
    public interface IDbSession : IDisposable, IAsyncDisposable
    {
        /// <summary>
        ///     Gets the <see cref="DbConnection"/> used by this <see cref="IDbSession"/>.
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        ///     Gets the <see cref="DbTransaction"/> used by this <see cref="IDbSession"/>.
        /// </summary>
        DbTransaction Transaction { get; }

        /// <summary>
        ///     Gets a collection of key/value pairs that provide additional information about the <see cref="IDbSession"/>.
        /// </summary>
        Dictionary<string, object> Data { get; }

        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<dynamic>> QueryAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> QuerySingleAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<dynamic> QuerySingleAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<dynamic> QuerySingleOrDefaultAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> QueryFirstAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<dynamic> QueryFirstAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<dynamic> QueryFirstOrDefaultAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
        Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>The first cell returned.</returns>
        Task<dynamic> ExecuteScalarAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Asynchronously execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>Multiple result set.</returns>
        Task<SqlMapper.GridReader> QueryMultipleAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a query and returns asynchronous stream.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="IAsyncEnumerable{T}"/> representing the asynchronous stream.</returns>
        IAsyncEnumerable<T> Query<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     Execute a query and returns asynchronous stream.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">An optional token to cancel the asynchronous operation. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns>A <see cref="IAsyncEnumerable{dynamic}"/> representing the asynchronous stream.</returns>
        IAsyncEnumerable<dynamic> Query(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);
    }
}