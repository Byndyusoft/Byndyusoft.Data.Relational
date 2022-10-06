using System;
using System.Data.Common;
using CommunityToolkit.Diagnostics;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionOptions
    {
        private string? _connectionString;
        private DbProviderFactory? _dbProviderFactory;

        public string ConnectionString
        {
            get => _connectionString!;
            set
            {
                Guard.IsNotNullOrWhiteSpace(value, nameof(ConnectionString));
                _connectionString = value;
            }
        }

        public DbProviderFactory DbProviderFactory
        {
            get => _dbProviderFactory!;
            set

            {
                Guard.IsNotNull(value, nameof(DbProviderFactory));
                _dbProviderFactory = value;
            }
        }

        internal DbSessionOptions Validate(string name)
        {
            if (_dbProviderFactory is null || _connectionString is null)
            {
                var message =
                    string.IsNullOrWhiteSpace(name)
                        ? $"{nameof(DbSession)} hasn't been configured"
                        : $"{nameof(DbSession)} with name {name} hasn't been configured";
                throw new InvalidOperationException(message);
            }

            return this;
        }
    }
}