using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionStorage : IDbSessionsIndexer, IDbSessionStorage
    {
        private static readonly AsyncLocal<ConcurrentDictionary<string, IDbSession?>?> _current = new();

        public IDbSession? this[string name]
        {
            get => _current.Value?.TryGetValue(name, out var session) == true ? session : null;
            set
            {
                var dic = _current.Value ??= new ();
                dic.AddOrUpdate(name, value, (_, current) =>
                {
                    if (value is not null && current is not null)
                        throw new InvalidOperationException($"{nameof(DbSession)} with name {name} already exists");
                    return value;
                });
            }
        }

        public IDbSession? GetCurrent(string name)
            => _current.Value?.TryGetValue(name, out var session) == true ? session : null;

        public void SetCurrent(string name, IDbSession? session)
        {
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