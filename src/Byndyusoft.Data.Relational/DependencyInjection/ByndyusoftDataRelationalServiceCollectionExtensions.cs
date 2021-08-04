using System;
using System.Data.Common;
using Byndyusoft.Data.Relational;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ByndyusoftDataRelationalServiceCollectionExtensions
    {
        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, string connectionString)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (dbProviderFactory == null) throw new ArgumentNullException(nameof(dbProviderFactory));
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            services.TryAddTransient<IDbSessionAccessor, DbSessionAccessor>();
            services.TryAddTransient<IDbSessionFactory>(x => new DbSessionFactory(dbProviderFactory, connectionString));

            return services;
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, Func<string> connectionStringFunc)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (dbProviderFactory == null) throw new ArgumentNullException(nameof(dbProviderFactory));
            if (connectionStringFunc == null) throw new ArgumentNullException(nameof(connectionStringFunc));

            return services.AddRelationalDb(dbProviderFactory, connectionStringFunc());
        }
    }
}