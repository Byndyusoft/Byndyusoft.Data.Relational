using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Dapper;

// ReSharper disable once CheckNamespace
namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Extensions to work with <see cref="IDbSession" /> via <see cref="QueryObject" />.
    /// </summary>
    public static class DbSessionQueryObjectExtensions
    {
        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout,
                commandType, 
                typeDeserializer, 
                cancellationToken);
        }

        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<IEnumerable<dynamic>> QueryAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout, 
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<T> QuerySingleAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QuerySingleAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic> QuerySingleAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QuerySingleAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout, 
                commandType, 
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<T?> QuerySingleOrDefaultAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QuerySingleOrDefaultAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic?> QuerySingleOrDefaultAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QuerySingleOrDefaultAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<T> QueryFirstAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryFirstAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic> QueryFirstAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryFirstAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<T?> QueryFirstOrDefaultAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryFirstOrDefaultAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout,
                commandType, 
                typeDeserializer, 
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic?> QueryFirstOrDefaultAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryFirstOrDefaultAsync(
                    queryObject,
                    session.Transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken);
        }

        /// <summary>
        ///     Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        public static Task<int> ExecuteAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.ExecuteAsync(queryObject,
                    session.Transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned, as <typeparamref name="T" />.</returns>
        public static Task<T> ExecuteScalarAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.ExecuteScalarAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout,
                commandType, 
                typeDeserializer, 
                cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned.</returns>
        public static Task<dynamic> ExecuteScalarAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.ExecuteScalarAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>Multiple result set.</returns>
        public static Task<SqlMapper.GridReader> QueryMultipleAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryMultipleAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

#if NET5_0_OR_GREATER


        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{dynamic}"/>.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<dynamic> QueryUnbufferedAsync(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryUnbufferedAsync(
                queryObject, 
                session.Transaction, 
                commandTimeout, 
                commandType);
        }

        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<T> QueryUnbufferedAsync<T>(
            this IDbSession session,
            QueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null)
        {
            Guard.IsNotNull(session, nameof(session));

            return session.Connection.QueryUnbufferedAsync(
                queryObject,
                session.Transaction,
                commandTimeout,
                commandType,
                typeDeserializer);
        }
#endif
    }
}