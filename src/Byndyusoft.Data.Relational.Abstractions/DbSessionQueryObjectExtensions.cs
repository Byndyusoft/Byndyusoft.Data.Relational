using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

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
        public static async Task<IEnumerable<T>> QueryAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType, typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<IEnumerable<dynamic>> QueryAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
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
        public static async Task<T> QuerySingleAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QuerySingleAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                    typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<dynamic> QuerySingleAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QuerySingleAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType, cancellationToken)
                .ConfigureAwait(false);
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
        public static async Task<T> QuerySingleOrDefaultAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QuerySingleOrDefaultAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType, typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<dynamic> QuerySingleOrDefaultAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QuerySingleOrDefaultAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                    cancellationToken).ConfigureAwait(false);
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
        public static async Task<T> QueryFirstAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QueryFirstAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                    typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<dynamic> QueryFirstAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QueryFirstAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType, cancellationToken)
                .ConfigureAwait(false);
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
        public static async Task<T> QueryFirstOrDefaultAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryFirstOrDefaultAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType, typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<dynamic> QueryFirstOrDefaultAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session
                .QueryFirstOrDefaultAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                    cancellationToken).ConfigureAwait(false);
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
        public static async Task<int> ExecuteAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteAsync(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
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
        public static async Task<T> ExecuteScalarAsync<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteScalarAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType, typeDeserializer, cancellationToken).ConfigureAwait(false);
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
        public static async Task<dynamic> ExecuteScalarAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteScalarAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType,
                cancellationToken).ConfigureAwait(false);
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
        public static async Task<SqlMapper.GridReader> QueryMultipleAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryMultipleAsync(queryObject.Sql, queryObject.Params, commandTimeout,
                commandType,
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a query and returns asynchronous stream.
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
        /// <returns>A <see cref="IAsyncEnumerable{T}" /> representing the asynchronous stream.</returns>
        public static IAsyncEnumerable<T> Query<T>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
             ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return session.Query(queryObject.Sql, queryObject.Params, commandTimeout, commandType, typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a query and returns asynchronous stream.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="IAsyncEnumerable{dynamic}" /> representing the asynchronous stream.</returns>
        public static IAsyncEnumerable<dynamic> Query(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return session.Query(queryObject.Sql, queryObject.Params, commandTimeout, commandType,
                cancellationToken);
        }
    }
}