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
    public static class DbConnectionQueryObjectExtensions
    {
        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QuerySingleAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QuerySingleAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QuerySingleOrDefaultAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QuerySingleOrDefaultAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryFirstAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryFirstAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryFirstOrDefaultAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryFirstOrDefaultAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.ExecuteAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.ExecuteScalarAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer,
                cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute SQL that selects a single value.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <param name="cancellationToken">
        ///     An optional token to cancel the asynchronous operation. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        /// <returns>The first cell returned.</returns>
        public static Task<dynamic> ExecuteScalarAsync(
            this DbConnection connection,
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.ExecuteScalarAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

        /// <summary>
        ///     Asynchronously execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryMultipleAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                cancellationToken);
        }

#if NET5_0_OR_GREATER

        /// <summary>
        /// Execute a query asynchronously using <see cref="IAsyncEnumerable{dynamic}"/>.
        /// </summary>
        /// <param name="connection">The connection to query on.</param>
        /// <param name="queryObject">The query object to execute.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of dynamic data
        /// </returns>
        public static IAsyncEnumerable<dynamic> QueryUnbufferedAsync(
            this DbConnection connection,
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryUnbufferedAsync(
                queryObject.Sql, 
                queryObject.Params, 
                transaction, 
                commandTimeout, 
                commandType);
        }

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
            QueryObject queryObject,
            DbTransaction? transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            ITypeDeserializer<T>? typeDeserializer = null)
        {
            Guard.IsNotNull(connection, nameof(connection));
            Guard.IsNotNull(queryObject, nameof(queryObject));

            return connection.QueryUnbufferedAsync(
                queryObject.Sql,
                queryObject.Params,
                transaction,
                commandTimeout,
                commandType,
                typeDeserializer);
        }

#endif
    }
}