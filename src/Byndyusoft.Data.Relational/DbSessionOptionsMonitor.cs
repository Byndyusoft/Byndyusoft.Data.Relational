using System;
using System.Data.Common;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionOptionsMonitor : IOptionsMonitor<DbSessionOptions>
    {
        private readonly DbSessionOptions _options;

        public DbSessionOptionsMonitor(DbProviderFactory providerFactory, string connectionString)
        {
            _options = new DbSessionOptions
            {
                ConnectionString = connectionString,
                DbProviderFactory = providerFactory
            };
        }

        public DbSessionOptions Get(string name) => _options;

        public IDisposable OnChange(Action<DbSessionOptions, string> listener) => throw new NotImplementedException();

        public DbSessionOptions CurrentValue => _options;
    }
}