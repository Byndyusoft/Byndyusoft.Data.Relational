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
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<IEnumerable<dynamic>> QueryAsync(
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
            return connection.QueryAsync(command);
        }

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
        public static Task<IEnumerable<T>> QueryAsync<T>(
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
                ? connection.QueryAsync<T>(command)
                : connection.QueryAsync(command).DeserializeAsync(typeDeserializer);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic> QuerySingleAsync(
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
            return connection.QuerySingleAsync(command);
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
        public static Task<T> QuerySingleAsync<T>(
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
                ? connection.QuerySingleAsync<T>(command)
                : connection.QuerySingleAsync(command).DeserializeNullableAsync(typeDeserializer)!;
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic?> QuerySingleOrDefaultAsync(
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

            return connection.QuerySingleOrDefaultAsync(command);
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
        public static Task<T?> QuerySingleOrDefaultAsync<T>(
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
                ? connection.QuerySingleOrDefaultAsync<T?>(command)
                : connection.QuerySingleOrDefaultAsync(command).DeserializeAsync(typeDeserializer);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic> QueryFirstAsync(
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
            return connection.QueryFirstAsync(command);
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
        public static Task<T> QueryFirstAsync<T>(
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
                ? connection.QueryFirstAsync<T>(command)
                : connection.QueryFirstAsync(command).DeserializeNullableAsync(typeDeserializer)!;
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public static Task<dynamic?> QueryFirstOrDefaultAsync(
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
            return connection.QueryFirstOrDefaultAsync(command);
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
        public static Task<T?> QueryFirstOrDefaultAsync<T>(
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
                ? connection.QueryFirstOrDefaultAsync<T?>(command)
                : connection.QueryFirstOrDefaultAsync(command).DeserializeAsync(typeDeserializer);
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
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned, as <typeparamref name="T" />.</returns>
        public static Task<T?> ExecuteScalarAsync<T>(
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

            return connection.ExecuteScalarAsync<T>(command);
        }

        /// <summary>
        ///     Execute a command asynchronously using Task.
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
        /// <returns>The number of rows affected.</returns>
        public static Task<int> ExecuteAsync(
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

            return connection.ExecuteAsync(command);
        }
        
        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        public static Task<dynamic> ExecuteScalarAsync(
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

            return connection.ExecuteScalarAsync(command) as dynamic;
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
        public static Task<T?> ExecuteScalarAsync<T>(
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
                ? connection.ExecuteScalarAsync<T>(command)
                : connection.ExecuteScalarAsync(command).DeserializeAsync(typeDeserializer);
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
        public static Task<SqlMapper.GridReader> QueryMultipleAsync(
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
            return connection.QueryMultipleAsync(command);
        }

#if NET5_0_OR_GREATER

        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
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
                ?  SqlMapper.QueryUnbufferedAsync<T>(connection, sql, param, transaction, commandTimeout,
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