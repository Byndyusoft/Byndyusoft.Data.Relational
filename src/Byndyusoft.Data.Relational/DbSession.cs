using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSession : ICommittableDbSession
    {
        private static readonly ActivitySource _activitySource = DbSessionInstrumentationOptions.CreateActivitySource();

        internal static readonly DbSessionStorage Current = new();
        private readonly string _connectionString = default!;
        private readonly IsolationLevel? _isolationLevel;
        private readonly string _name = default!;
        private readonly DbProviderFactory _providerFactory = default!;
        private Activity? _activity;

        private DbConnection? _connection;
        private DbSessionItems? _items;
        private DbSessionState _state;
        private DbTransaction? _transaction;

        private DbSession()
        {
        }

        internal DbSession(DbConnection connection, DbTransaction? transaction = null)
            : this()
        {
            Guard.IsNotNull(connection, nameof(connection));

            _name = Options.DefaultName;
            _connection = connection;
            _transaction = transaction;
            _isolationLevel = transaction?.IsolationLevel;

            _state = DbSessionState.Active;
        }

        internal DbSession(string name, DbSessionOptions options, IsolationLevel? isolationLevel = null)
        {
            Guard.IsNotNull(options, nameof(options));
            Guard.IsNotNull(name, nameof(name));

            _providerFactory = options.DbProviderFactory;
            _connectionString = options.ConnectionString;
            _isolationLevel = isolationLevel;
            _name = name;
        }

        public async ValueTask DisposeAsync()
        {
            if (_state == DbSessionState.Disposed)
                return;

            await FinishAsync();
            await DisposeAsyncCore();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if (_state == DbSessionState.Disposed)
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

        public DbSessionState State
        {
            get
            {
                ThrowIfDisposed();
                return _state;
            }
        }

        public event DbSessionFinishedEventHandler? Finished;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_state != DbSessionState.Active)
                return;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Committing));

            if (_transaction != null) await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

            _state = DbSessionState.Committed;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Commited));
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_state != DbSessionState.Active)
                return;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.RollingBack));

            if (_transaction != null) await _transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

            _state = DbSessionState.RolledBack;

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.RolledBack));
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

            Finished?.Invoke(this, new DbSessionFinishedEventArgs(_state));

            Current[_name] = null;
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

            Finished?.Invoke(this, new DbSessionFinishedEventArgs(_state));

            Current[_name] = null;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (_state == DbSessionState.Active)
                return;

            _activity = _activitySource.StartActivity(nameof(DbSession));
            _activity?.SetTag("provider", _providerFactory.GetType().Name);
            _activity?.SetTag("isolationlevel", _isolationLevel);

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Starting));

            _connection = await CreateConnectionAsync(cancellationToken).ConfigureAwait(false);
            _transaction = await BeginTransactionAsync(_connection, cancellationToken).ConfigureAwait(false);

            _activity?.AddEvent(new ActivityEvent(DbSessionEvents.Started));

            _state = DbSessionState.Active;
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
                _state = DbSessionState.Disposed;
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
            if (_state == DbSessionState.Disposed)
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