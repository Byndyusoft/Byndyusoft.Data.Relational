using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Dapper;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Extensions to work with <see cref="IDbSession" /> queries.
    /// </summary>
    public static class DbSessionQueryExtensions
    {
        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
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
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            return session.Connection.QueryAsync(
                sql,
                param,
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
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<IEnumerable<dynamic>> QueryAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QueryAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
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
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await session.Connection.QuerySingleAsync<T>(command).ConfigureAwait(false)
                : await session.Connection.QuerySingleAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<dynamic> QuerySingleAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QuerySingleAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T?> QuerySingleOrDefaultAsync<T>(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await session.Connection.QuerySingleOrDefaultAsync<T>(command).ConfigureAwait(false)
                : await session.Connection.QuerySingleOrDefaultAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<dynamic?> QuerySingleOrDefaultAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
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
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await session.Connection.QueryFirstAsync<T>(command).ConfigureAwait(false)
                : await session.Connection.QueryFirstAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<dynamic> QueryFirstAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QueryFirstAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T?> QueryFirstOrDefaultAsync<T>(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await session.Connection.QueryFirstOrDefaultAsync<T>(command).ConfigureAwait(false)
                : await session.Connection.QueryFirstOrDefaultAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<dynamic?> QueryFirstOrDefaultAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QueryFirstOrDefaultAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> ExecuteAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.ExecuteAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
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
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await session.Connection.ExecuteScalarAsync<T>(command).ConfigureAwait(false)
                : await session.Connection.ExecuteScalarAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned.</returns>
        public static async Task<dynamic> ExecuteScalarAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.ExecuteScalarAsync(command).ConfigureAwait(false);
        }

        /// <summary>
        ///     Asynchronously execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>Multiple result set.</returns>
        public static async Task<SqlMapper.GridReader> QueryMultipleAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, session.Transaction, commandTimeout, commandType,
                cancellationToken);
            return await session.Connection.QueryMultipleAsync(command).ConfigureAwait(false);
        }

#if NET5_0_OR_GREATER

        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{dynamic}"/>.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<dynamic> QueryUnbufferedAsync(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            return session.Connection.QueryUnbufferedAsync(sql, param, session.Transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <param name="session">The session to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<T> QueryUnbufferedAsync<T>(
            this IDbSession session,
            string sql,
            object? param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            return typeDeserializer is null
                ? session.Connection.QueryUnbufferedAsync<T>(sql, param, session.Transaction, commandTimeout,
                    commandType)
                : session.QueryUnbufferedAsyncCore(sql, param, commandTimeout,
                    commandType, typeDeserializer);
        }

        private static async IAsyncEnumerable<T> QueryUnbufferedAsyncCore<T>(
            this IDbSession session,
            string sql,
            object? param,
            int? commandTimeout,
            CommandType? commandType,
            ITypeDeserializer<T> typeDeserializer)
        {
            Guard.IsNotNull(session, nameof(session));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var rows = session.QueryUnbufferedAsync(sql, param, commandTimeout, commandType);
            await foreach (var row in rows)
            {
                yield return typeDeserializer.Deserialize(row);
            }
        }

#endif

        private static CommandDefinition CreateCommand(string sql, object? param, IDbTransaction? transaction,
            int? commandTimeout, CommandType? commandType, CancellationToken cancellationToken, CommandFlags flags = CommandFlags.Buffered)
        {
            return new(sql, param, transaction, commandTimeout, commandType, flags,
                cancellationToken);
        }
    }
}