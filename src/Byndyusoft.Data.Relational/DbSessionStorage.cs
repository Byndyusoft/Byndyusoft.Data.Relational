using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionStorage : IDbSessionsIndexer
    {
        private static readonly AsyncLocal<ConcurrentDictionary<string, DbSession?>?> _current = new();

        public DbSession? this[string name]
        {
            get
            {
                DbSession? session = null;
                if (_current.Value?.TryGetValue(name, out session) == true)
                    return session;
                return null;
            }
            set
            {
                var dic = _current.Value ??= new ConcurrentDictionary<string, DbSession?>();
                dic.AddOrUpdate(name, value, (_, current) =>
                {
                    if (value is not null && current is not null)
                        throw new InvalidOperationException($"{nameof(DbSession)} with name {name} already exists");
                    return value;
                });
            }
        }

        IDbSession? IDbSessionsIndexer.this[string name] => this[name];
    }
}