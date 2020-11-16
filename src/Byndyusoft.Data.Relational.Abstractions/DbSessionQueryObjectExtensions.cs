using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Byndyusoft.Data.Relational
{
    public static class DbSessionQueryObjectExtensions
    {
        public static async Task<IEnumerable<TSource>> QueryAsync<TSource>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryAsync<TSource>(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<dynamic>> QueryAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryAsync(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

        public static async Task<int> ExecuteAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteAsync(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

        public static async Task<TSource> ExecuteScalarAsync<TSource>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteScalarAsync<TSource>(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

        public static async Task<dynamic> ExecuteScalarAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.ExecuteScalarAsync(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

        public static async Task<SqlMapper.GridReader> QueryMultipleAsync(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return await session.QueryMultipleAsync(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType,
                cancellationToken).ConfigureAwait(false);
        }

#if !NETSTANDARD2_0
        public static IAsyncEnumerable<TSource> Query<TSource>(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return session.Query<TSource>(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType, cancellationToken);
        }

        public static IAsyncEnumerable<dynamic> Query(
            this IDbSession session,
            IQueryObject queryObject,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (queryObject == null) throw new ArgumentNullException(nameof(queryObject));

            return session.Query(queryObject.Sql, queryObject.QueryParams, commandTimeout, commandType, cancellationToken);
        }
#endif
    }
}
