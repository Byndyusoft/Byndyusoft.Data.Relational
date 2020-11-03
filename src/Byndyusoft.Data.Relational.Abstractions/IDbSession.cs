namespace Byndyusoft.Data.Relational
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;

    public partial interface IDbSession : IDisposable
    {
        DbConnection Connection { get; }

        DbTransaction Transaction { get; }

        Task<IEnumerable<TSource>> QueryAsync<TSource>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<dynamic>> QueryAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        Task<TSource> ExecuteScalarAsync<TSource>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);

        Task<SqlMapper.GridReader> QueryMultipleAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CancellationToken cancellationToken = default);
    }

#if NETSTANDARD2_1
    public partial interface IDbSession : IAsyncDisposable
    {
        IAsyncEnumerable<TSource> Query<TSource>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null);

        IAsyncEnumerable<dynamic> Query(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null);
    }
#endif
}