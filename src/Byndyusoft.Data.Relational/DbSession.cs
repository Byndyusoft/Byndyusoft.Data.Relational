using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public sealed partial class DbSession : ICommittableDbSession
    {
        private bool _completed;
        private DbConnection _connection;
        private DbSessionData _data;
        private bool _disposed;
        private DbTransaction _transaction;

        internal DbSession()
        {
            Current = this;
        }

        internal DbSession(DbConnection connection, DbTransaction transaction = null)
            : this()
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _transaction = transaction;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            await DisposeAsyncCore().ConfigureAwait(false);
            GC.SuppressFinalize(this);
            _disposed = true;
            Current = null;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
            Current = null;
        }

        public DbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _connection;
            }
            internal set => _connection = value;
        }

        public DbTransaction Transaction
        {
            get
            {
                ThrowIfDisposed();
                return _transaction;
            }
            internal set => _transaction = value;
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                ThrowIfDisposed();
                return _transaction?.IsolationLevel ?? IsolationLevel.Unspecified;
            }
        }

        public IDictionary<string, object> Data
        {
            get
            {
                ThrowIfDisposed();
                return _data ??= new DbSessionData();
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_transaction == null || _completed)
                return;

            await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            _completed = true;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_transaction == null || _completed)
                return;

            await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            _completed = true;
        }

        ~DbSession()
        {
            Dispose(false);
            Current = null;
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            _transaction?.Dispose();
            _transaction = null;

            _connection?.Dispose();
            _connection = null;
            
            _data?.Dispose();
            _data = null;
        }

        private async Task DisposeAsyncCore()
        {
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

            if (_data != null)
            {
                await _data.DisposeAsync();
                _data = null;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }
    }
}