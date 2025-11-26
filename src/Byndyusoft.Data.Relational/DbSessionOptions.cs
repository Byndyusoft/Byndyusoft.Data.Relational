using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using CommunityToolkit.Diagnostics;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionOptions
    {
        private string? _connectionString;
        private DbProviderFactory? _dbProviderFactory;
        private Func<DbConnection>? _createConnectionFunc;

        [Required(AllowEmptyStrings = false)]
        public string ConnectionString
        {
            get => _connectionString!;
            set
            {
                Guard.IsNotNullOrWhiteSpace(value, nameof(ConnectionString));
                _connectionString = value;
            }
        }

        [Required]
        public DbProviderFactory DbProviderFactory
        {
            get => _dbProviderFactory!;
            set
            {
                Guard.IsNotNull(value, nameof(DbProviderFactory));
                _dbProviderFactory = value;
            }
        }

        public Func<DbConnection> CreateConnectionFunc
        {
            get => _createConnectionFunc ?? CreateConnection;
            set
            {
                Guard.IsNotNull(value, nameof(CreateConnectionFunc));
                _createConnectionFunc = value;
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

        private DbConnection CreateConnection()
        {
            var connection = _dbProviderFactory!.CreateConnection();
            if (connection == null)
                throw new InvalidOperationException();
            connection.ConnectionString = _connectionString;
            return connection;
        }
    }
}