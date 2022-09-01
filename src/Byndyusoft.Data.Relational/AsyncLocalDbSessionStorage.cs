using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    public class AsyncLocalDbSessionStorage : IDbSessionStorage
    {
        private static readonly AsyncLocal<ConcurrentDictionary<string, IDbSession?>?> _current = new();
        
        IDbSession? IDbSessionsIndexer.this[string name] => GetCurrentSession(name);
        
        public virtual IDbSession? GetCurrentSession(string name)
        {
            IDbSession? session = null;
            if (_current.Value?.TryGetValue(name, out session) == true)
                return session;
            return null;
        }

        public virtual void SetCurrentSession(string name, IDbSession? session)
        {
            var dic = _current.Value ??= new ConcurrentDictionary<string, IDbSession?>(StringComparer.InvariantCultureIgnoreCase);
            dic.AddOrUpdate(name, session, (_, current) =>
            {
                if (session is not null && current is not null)
                    throw new InvalidOperationException($"{nameof(DbSession)} with name {name} already exists");
                return session;
            });
        }
    }
}