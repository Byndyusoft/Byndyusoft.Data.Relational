using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational;
using CommunityToolkit.Diagnostics;
using Dapper;

// ReSharper disable once CheckNamespace
namespace System.Data.Common
{
    /// <summary>
    ///     Extensions to work with <see cref="DbConnection" /> queries.
    /// </summary>
    public static class DbConnectionQueryExtensions
    {
        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<IEnumerable<T>> QueryAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.QueryAsync<T>(command).ConfigureAwait(false)
                : await connection.QueryAsync(command).DeserializeAsync(typeDeserializer).ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T> QuerySingleAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.QuerySingleAsync<T>(command).ConfigureAwait(false)
                : await connection.QuerySingleAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T?> QuerySingleOrDefaultAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.QuerySingleOrDefaultAsync<T>(command).ConfigureAwait(false)
                : await connection.QuerySingleOrDefaultAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T> QueryFirstAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.QueryFirstAsync<T>(command).ConfigureAwait(false)
                : await connection.QueryFirstAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static async Task<T?> QueryFirstOrDefaultAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.QueryFirstOrDefaultAsync<T>(command).ConfigureAwait(false)
                : await connection.QueryFirstOrDefaultAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }


        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned, as <typeparamref name="T" />.</returns>
        public static async Task<T> ExecuteScalarAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);

            return typeDeserializer == null
                ? await connection.ExecuteScalarAsync<T>(command).ConfigureAwait(false)
                : await connection.ExecuteScalarAsync(command).DeserializeAsync(typeDeserializer)
                    .ConfigureAwait(false);
        }

        /// <summary>
        ///     Asynchronously execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>Multiple result set.</returns>
        public static async Task<SqlMapper.GridReader> QueryMultipleAsync(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            var command = CreateCommand(sql, param, transaction, commandTimeout, commandType,
                cancellationToken);
            return await connection.QueryMultipleAsync(command).ConfigureAwait(false);
        }

#if NET5_0_OR_GREATER
        
        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="typeDeserializer">The type deserializer.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<T> QueryUnbufferedAsync<T>(
            this DbConnection connection,
            string sql,
            object? param = null,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            return typeDeserializer is null
                ? connection.QueryUnbufferedAsync<T>(sql, param, transaction, commandTimeout,
                    commandType)
                : connection.QueryUnbufferedAsyncCore(sql, param, transaction, commandTimeout,
                    commandType, typeDeserializer);
        }

        private static async IAsyncEnumerable<T> QueryUnbufferedAsyncCore<T>(
            this DbConnection connection,
            string sql,
            object? param,
            DbTransaction? transaction,
            int? commandTimeout,
            CommandType? commandType,
            ITypeDeserializer<T> typeDeserializer)
        {
            var rows = connection.QueryUnbufferedAsync(sql, param, transaction, commandTimeout, commandType);
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