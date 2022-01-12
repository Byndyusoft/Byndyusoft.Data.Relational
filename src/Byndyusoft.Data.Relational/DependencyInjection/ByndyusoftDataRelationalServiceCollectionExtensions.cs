using Byndyusoft.Data.Relational;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Data.Common;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ByndyusoftDataRelationalServiceCollectionExtensions
    {
        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, string connectionString)
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            services.TryAddTransient<IDbSessionAccessor, DbSessionAccessor>();
            services.TryAddTransient<IDbSessionFactory>(x => new DbSessionFactory(dbProviderFactory, connectionString));

            return services;
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, Func<string> connectionStringFunc)
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.NotNull(connectionStringFunc, nameof(connectionStringFunc));

            return services.AddRelationalDb(dbProviderFactory, connectionStringFunc());
        }
    }
}