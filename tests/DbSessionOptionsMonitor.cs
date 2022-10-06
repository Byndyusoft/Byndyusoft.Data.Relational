using System;
using System.Collections.Generic;
using System.Data.Common;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionOptionsMonitor : IOptionsMonitor<DbSessionOptions>
    {
        private readonly Dictionary<string, DbSessionOptions> _options = new();

        public DbSessionOptionsMonitor()
        {
        }

        public DbSessionOptionsMonitor(DbProviderFactory providerFactory, string connectionString)
            : this()
        {
            Add(Options.DefaultName, providerFactory, connectionString);
        }

        public DbSessionOptions Get(string name)
        {
            if (_options.TryGetValue(name, out var session) == false)
                return null;
            return session;
        }

        public IDisposable OnChange(Action<DbSessionOptions, string> listener)
        {
            throw new NotImplementedException();
        }

        public DbSessionOptions CurrentValue => Get(Options.DefaultName);

        public void Add(string name, DbProviderFactory providerFactory, string connectionString)
        {
            Guard.IsNotNull(providerFactory, nameof(providerFactory));
            Guard.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            _options.Add(name, new DbSessionOptions
            {
                ConnectionString = connectionString,
                DbProviderFactory = providerFactory
            });
        }
    }
}