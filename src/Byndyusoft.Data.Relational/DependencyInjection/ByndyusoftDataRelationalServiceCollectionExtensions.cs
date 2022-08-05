using System;
using System.Data.Common;
using Byndyusoft.Data.Relational;
using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ByndyusoftDataRelationalServiceCollectionExtensions
    {
        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, string connectionString)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            services.TryAddTransient<IDbSessionAccessor, DbSessionAccessor>();
            services.TryAddTransient<IDbSessionFactory>(_ => new DbSessionFactory(dbProviderFactory, connectionString));

            return services;
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, Func<string> connectionStringFunc)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.IsNotNull(connectionStringFunc, nameof(connectionStringFunc));

            return services.AddRelationalDb(dbProviderFactory, connectionStringFunc());
        }
    }
}