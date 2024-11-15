using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly IOptionsMonitor<DbSessionOptions> _options;
        private readonly IDbSessionStorage _sessionStorage;

        public DbSessionFactory(
            IOptionsMonitor<DbSessionOptions> options,
            IDbSessionStorage sessionStorage)
        {
            Guard.IsNotNull(options, nameof(options));
            Guard.IsNotNull(sessionStorage, nameof(sessionStorage));

            _options = options;
            _sessionStorage = sessionStorage;
        }

        public virtual Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateSessionAsync(Options.DefaultName, cancellationToken);
        }

        public virtual Task<IDbSession> CreateSessionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return CreateSessionAsync(Options.DefaultName, isolationLevel, cancellationToken);
        }

        public virtual Task<IDbSession> CreateSessionAsync(string name, CancellationToken cancellationToken = default)
        {
            return CreateSessionAsyncCore(name, isolationLevel: null, cancellationToken);
        }

        public Task<IDbSession> CreateSessionAsync(string name, IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return CreateSessionAsyncCore(name, isolationLevel, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(Options.DefaultName, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(string name,
            CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(name, IsolationLevel.Unspecified, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(Options.DefaultName, isolationLevel, cancellationToken);
        }

        public virtual Task<ICommittableDbSession> CreateCommittableSessionAsync(
            string name, IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(name, nameof(name));

            var options = _options.Get(name).Validate(name);
            var session = new DbSession(name, _sessionStorage, options, isolationLevel);
            _sessionStorage.SetCurrent(name, session);
            return StartAsyncCore<ICommittableDbSession>(session, cancellationToken);
        }

        private Task<IDbSession> CreateSessionAsyncCore(string name, IsolationLevel? isolationLevel, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(name, nameof(name));

            var options = _options.Get(name).Validate(name);
            var session = new DbSession(name, _sessionStorage, options, isolationLevel);
            _sessionStorage.SetCurrent(name, session);
            return StartAsyncCore<IDbSession>(session, cancellationToken);
        }

        private static async Task<T> StartAsyncCore<T>(DbSession session, CancellationToken cancellationToken)
            where T : IDbSession
        {
            try
            {
                await session.StartAsync(cancellationToken).ConfigureAwait(false);
                return (T) (IDbSession) session;
            }
            catch
            {
                await session.DisposeAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}