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

        public DbSessionFactory(IOptionsMonitor<DbSessionOptions> options)
        {
            Guard.IsNotNull(options, nameof(options));

            _options = options;
        }

        public virtual Task<IDbSession> CreateSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateSessionAsync(Options.DefaultName, cancellationToken);
        }

        public virtual Task<IDbSession> CreateSessionAsync(string name, CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(name, nameof(name));

            var options = _options.Get(name).Validate(name);
            var session = new DbSession(name, options);
            DbSession.Current[name] = session;
            return StartAsyncCore<IDbSession>(session, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(CancellationToken cancellationToken = default)
        {
            return CreateCommittableSessionAsync(Options.DefaultName, cancellationToken);
        }

        public Task<ICommittableDbSession> CreateCommittableSessionAsync(string name,
            CancellationToken cancellationToken = default)
        {
            Guard.IsNotNull(name, nameof(name));

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
            var options = _options.Get(name).Validate(name);
            var session = new DbSession(name, options, isolationLevel);
            DbSession.Current[name] = session;
            return StartAsyncCore<ICommittableDbSession>(session, cancellationToken);
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