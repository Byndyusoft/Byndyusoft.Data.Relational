using System.Runtime.Versioning;

namespace Byndyusoft.Data.Relational
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;

    public partial class DbSession : IDbSession
    {
        private bool _disposed;
        private DbConnection _connection;
        private DbTransaction _transaction;
        private IsolationLevel? _isolationLevel;

        public DbSession(DbConnection connection, IsolationLevel? isolationLevel = default)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _isolationLevel = isolationLevel;
        }

        ~DbSession()
        {
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
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

        public async Task<IEnumerable<TSource>> QueryAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
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
            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryAsync(command).ConfigureAwait(false);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null,
            CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.ExecuteAsync(command).ConfigureAwait(false);
        }

        public async Task<TSource> ExecuteScalarAsync<TSource>(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.ExecuteScalarAsync<TSource>(command).ConfigureAwait(false);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null,
            int? commandTimeout = null, CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            await EnsureOpenedAsync(cancellationToken).ConfigureAwait(false);
            var command = CreateCommand(sql, param, commandTimeout, commandType, cancellationToken);
            return await Connection.QueryMultipleAsync(command).ConfigureAwait(false);
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

        private void Dispose(bool disposing)
        {
            if (_disposed || disposing == false)
                return;

            _transaction?.Dispose();
            _transaction = null;

            _connection?.Dispose();
            _connection = null;

            _disposed = true;
            DbSessionAccessor.DbSession = null;
        }

        internal async Task EnsureOpenedAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_connection.State == ConnectionState.Open)
                return;

            await _connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            if (_isolationLevel.HasValue)
            {
#if NETSTANDARD2_1
                _transaction = await _connection.BeginTransactionAsync(_isolationLevel.Value, cancellationToken)
                    .ConfigureAwait(false);
#else
                _transaction = _connection.BeginTransaction(_isolationLevel.Value);
#endif
            }
        }
    }

#if NETSTANDARD2_1
    public partial class DbSession
    {
        public async IAsyncEnumerable<TSource> Query<TSource>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            await EnsureOpenedAsync().ConfigureAwait(false);
            var items = await QueryAsync<TSource>(sql, param, commandTimeout, commandType).ConfigureAwait(false);
            foreach (var item in items)
            {
                yield return item;
            }
        }

        public async IAsyncEnumerable<dynamic> Query(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null)
        {
            await EnsureOpenedAsync().ConfigureAwait(false);
            var items = await QueryAsync(sql, param, commandTimeout, commandType).ConfigureAwait(false);
            foreach (var item in items)
            {
                yield return item;
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_disposed)
                return;

            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }

            ((IDisposable) this).Dispose();
        }
    }
#endif
}