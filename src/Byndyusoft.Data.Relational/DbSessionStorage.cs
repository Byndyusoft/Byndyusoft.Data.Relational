using CommunityToolkit.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionStorage : IDbSessionStorage
    {
        private static readonly AsyncLocal<ConcurrentDictionary<string, IDbSession?>?> _current = new();

        IDbSession? IDbSessionsIndexer.this[string name]
            => GetCurrent(name);

        public IDbSession? GetCurrent(string name)
        {
            Guard.IsNotNull(name, nameof(name));

            return _current.Value?.TryGetValue(name, out var session) == true ? session : null;
        }

        public void SetCurrent(string name, IDbSession? session)
        {
            Guard.IsNotNull(name, nameof(name));

            var dic = _current.Value ??= new ();
            dic.AddOrUpdate(name, session, (_, current) =>
            {
                if (session is not null && current is not null)
                    throw new InvalidOperationException($"{nameof(DbSession)} with name {name} already exists");
                return session;
            });
        }
    }
}