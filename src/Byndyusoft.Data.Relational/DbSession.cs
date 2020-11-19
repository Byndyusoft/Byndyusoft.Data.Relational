using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Byndyusoft.Data.Relational
{
    public class DbSession : IDbSession
    {
        private DbConnection _connection;
        private Dictionary<string, object> _data;
        private bool _disposed;
        private readonly IsolationLevel? _isolationLevel;
        private DbTransaction _transaction;

        public DbSession(DbConnection connection, IsolationLevel? isolationLevel = default)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _isolationLevel = isolationLevel;
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
            _disposed = true;
        }

        public DbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _connection;
            }
        }

        public DbTransaction Transaction
        {
            get
            {
                ThrowIfDisposed();
                return _transaction;
            }
        }

        public Dictionary<string, object> Data
        {
            get
            {
                ThrowIfDisposed();
                return _data ??= new Dictionary<string, object>();
            }
        }

        public async Task<IEnumerable<TSource>> QueryAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> QuerySingleAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QuerySingleAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<dynamic> QuerySingleAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QuerySingleAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> QuerySingleOrDefaultAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QuerySingleOrDefaultAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QuerySingleOrDefaultAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> QueryFirstAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryFirstAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<dynamic> QueryFirstAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryFirstAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> QueryFirstOrDefaultAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryFirstOrDefaultAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryFirstOrDefaultAsync(command).ConfigureAwait(false);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null,
            CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.ExecuteAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> ExecuteScalarAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.ExecuteScalarAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<dynamic> ExecuteScalarAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.ExecuteScalarAsync(command).ConfigureAwait(false);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryMultipleAsync(command).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<TSource> Query<TSource>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var items = await QueryAsync<TSource>(sql, param, commandTimeout, commandType, cancellationToken)
                .ConfigureAwait(false);
            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }

        public async IAsyncEnumerable<dynamic> Query(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentNullException(nameof(sql));

            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var items = await QueryAsync(sql, param, commandTimeout, commandType, cancellationToken)
                .ConfigureAwait(false);
            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await DisposeAsync(true).ConfigureAwait(false);
            _disposed = true;
        }

        ~DbSession()
        {
            Dispose(false);
        }

        private CommandDefinition CreateCommand(string sql, object param,
            int? commandTimeout, CommandType? commandType, CancellationToken cancellationToken)
        {
            return new CommandDefinition(sql, param, _transaction, commandTimeout, commandType, CommandFlags.Buffered,
                cancellationToken);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || disposing == false)
                return;

            _transaction?.Dispose();
            _transaction = null;

            _connection?.Dispose();
            _connection = null;

            DbSessionAccessor.DbSession = null;
        }

        internal async Task EnsureOpenedAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_connection.State != ConnectionState.Closed)
                return;

            await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            if (_isolationLevel.HasValue)
                _transaction = await _connection.BeginTransactionAsync(_isolationLevel.Value, cancellationToken);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (_disposed || disposing == false)
                return;


            if (_transaction != null)
            {
                await _transaction.DisposeAsync().ConfigureAwait(false);
                _transaction = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync().ConfigureAwait(false);
                _connection = null;
            }

            Dispose(true);
        }
    }
}