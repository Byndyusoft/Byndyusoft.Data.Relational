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

            return services.AddRelationalDb(Options.Options.DefaultName, dbProviderFactory, connectionString);
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            string name, DbProviderFactory dbProviderFactory, string connectionString)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            return services.AddRelationalDb(name, options =>
            {
                options.DbProviderFactory = dbProviderFactory;
                options.ConnectionString = connectionString;
            });
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            DbProviderFactory dbProviderFactory, Func<string> connectionStringFunc)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.IsNotNull(connectionStringFunc, nameof(connectionStringFunc));

            return services.AddRelationalDb(Options.Options.DefaultName, dbProviderFactory, connectionStringFunc);
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            string name, DbProviderFactory dbProviderFactory, Func<string> connectionStringFunc)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(dbProviderFactory, nameof(dbProviderFactory));
            Guard.IsNotNull(connectionStringFunc, nameof(connectionStringFunc));

            return services.AddRelationalDb(name, options =>
            {
                options.DbProviderFactory = dbProviderFactory;
                options.ConnectionString = connectionStringFunc();
            });
        }

        public static IServiceCollection AddRelationalDb(this IServiceCollection services,
            string name, Action<DbSessionOptions> configureOptions)
        {
            Guard.IsNotNull(services, nameof(services));
            Guard.IsNotNull(name, nameof(name));
            Guard.IsNotNull(configureOptions, nameof(configureOptions));

            services.AddOptions<DbSessionOptions>(name)
                .Configure(configureOptions)
                .ValidateDataAnnotations();

            services.TryAddSingleton<IDbSessionAccessor, DbSessionAccessor>();
            services.TryAddSingleton<IDbSessionFactory, DbSessionFactory>();
            services.TryAddSingleton<IDbSessionStorage, DbSessionStorage>();

            return services;
        }
    }
}