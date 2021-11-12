using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public sealed partial class DbSession : ICommittableDbSession
    {
        private static readonly AssemblyName AssemblyName = typeof(DbSession).Assembly.GetName();
        public static readonly string ActivitySourceName = AssemblyName.Name!;
        private static readonly Version Version = AssemblyName.Version!;
        private static readonly ActivitySource ActivitySource =
            new ActivitySource(ActivitySourceName, Version.ToString());

        private bool _completed;
        private DbConnection? _connection;
        private bool _disposed;
        private DbSessionItems? _items;
        private DbTransaction? _transaction;
        private Activity? _activity;

        internal DbSession()
        {
            Current = this;

            _activity = ActivitySource.StartActivity(nameof(DbSession));
        }

        internal DbSession(DbConnection connection, DbTransaction? transaction = null)
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
            Dispose();
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Dispose));

            Dispose(true);
            _disposed = true;
            Current = null;
            GC.SuppressFinalize(this);
        }

        public DbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _connection!;
            }
            internal set => _connection = value;
        }

        public DbTransaction? Transaction
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

        public IDictionary<string, object> Items
        {
            get
            {
                ThrowIfDisposed();
                return _items ??= new DbSessionItems();
            }
        }

        public ValueTask StartAsync(CancellationToken _ = default)
        {
            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Start));
            return new ValueTask();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Commit));

            if (_transaction == null || _completed)
                return;

            await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            _completed = true;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Rollback));

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

            _items?.Dispose();
            _items = null;

            _activity?.Dispose();
            _activity = null;
        }

        private async Task DisposeAsyncCore()
        {
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

            if (_items != null)
            {
                await _items.DisposeAsync().ConfigureAwait(false);
                _items = null;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName);
        }
    }
}