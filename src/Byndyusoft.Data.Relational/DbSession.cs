using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    public partial class DbSession : ICommittableDbSession
    {
        private readonly DbProviderFactory _providerFactory = default!;
        private readonly string _connectionString = default!;
        private readonly IsolationLevel? _isolationLevel;
        private static readonly ActivitySource ActivitySource = DbSessionInstrumentationOptions.CreateActivitySource();

        private bool _completed;
        private DbConnection? _connection;
        private bool _disposed;
        private DbSessionItems? _items;
        private DbTransaction? _transaction;
        private Activity? _activity;

        private DbSession()
        {
        }

        internal DbSession(DbConnection connection, DbTransaction? transaction = null)
            : this()
        {
            _connection = Guard.NotNull(connection, nameof(connection));
            _transaction = transaction;
            _isolationLevel = transaction?.IsolationLevel;
        }

        internal DbSession(DbProviderFactory providerFactory, string connectionString,
            IsolationLevel? isolationLevel = null)
            : this()
        {
            _providerFactory = providerFactory;
            _connectionString = connectionString;
            _isolationLevel = isolationLevel;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            await FinishAsync();
            await DisposeAsyncCore();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Finish();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _connection!;
            }
        }

        public DbTransaction? Transaction
        {
            get
            {
                ThrowIfDisposed();
                return _transaction;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                ThrowIfDisposed();
                return _isolationLevel ?? IsolationLevel.Unspecified;
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

        public void Finish()
        {
            ThrowIfDisposed();

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Finishing));

            _transaction?.Dispose();
            _transaction = null;

            _connection?.Dispose();
            _connection = null;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Finished));
            _activity?.Dispose();
            _activity = null;

            Current = null;
        }

        public async Task FinishAsync()
        {
            ThrowIfDisposed();

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Finishing));

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

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Finished));
            _activity?.Dispose();
            _activity = null;

            Current = null;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            _activity = ActivitySource.StartActivity(nameof(DbSession));
            _activity?.SetTag("provider", _providerFactory.GetType().Name);
            _activity?.SetTag("isolationlevel", _isolationLevel);

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Starting));

            _connection = await CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
            _transaction = await BeginTransactionAsync(_connection, cancellationToken).ConfigureAwait(false);

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Started));
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_completed)
                return;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Committing));

            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }

            _completed = true;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Commited));
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_completed)
                return;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.RollingBack));

            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            }

            _completed = true;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.RolledBack));
        }

        ~DbSession()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
                _items?.Dispose();
                _activity?.Dispose();
                _disposed = true;
            }

            _transaction = null;
            _connection = null;
            _items = null;
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

        private async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            var connection = _providerFactory.CreateConnection()!;
            if (connection == null)
                throw new InvalidOperationException();
            connection.ConnectionString = _connectionString;
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return connection;
        }

        private async Task<DbTransaction?> BeginTransactionAsync(DbConnection connection,
            CancellationToken cancellationToken)
        {
            return _isolationLevel != null
                ? await connection.BeginTransactionAsync(_isolationLevel.Value, cancellationToken)
                    .ConfigureAwait(false)
                : null;
        }
    }
}