using Microsoft.Extensions.Options;
using System;

namespace Byndyusoft.Data.Relational.Internal
{
    internal sealed class DbOptionsMonitor<T> : IOptionsMonitor<T>
    {
        public DbOptionsMonitor(T currentValue)
        {
            CurrentValue = currentValue;
        }

        public T Get(string name)
        {
            return CurrentValue;
        }

        public IDisposable OnChange(Action<T, string> listener)
        {
            throw new NotImplementedException();
        }

        public T CurrentValue { get; }
    }
}