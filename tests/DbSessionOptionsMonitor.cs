using System;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionOptionsMonitor : IOptionsMonitor<DbSessionOptions>
    {
        public DbSessionOptionsMonitor(DbProviderFactory providerFactory, string connectionString)
        {
            CurrentValue = new DbSessionOptions
            {
                ConnectionString = connectionString,
                DbProviderFactory = providerFactory
            };
        }

        public DbSessionOptions Get(string name) => CurrentValue;

        public IDisposable OnChange(Action<DbSessionOptions, string> listener) => throw new NotImplementedException();

        public DbSessionOptions CurrentValue { get; }
    }
}