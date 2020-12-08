using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational.Byndyusoft.Data.Sessions;
using Dapper;
using NHibernate;

namespace Byndyusoft.Data.Relational
{
    public class NhSession : AsyncDisposableSession, ICommittableDbSession
    {
        private ISession _session;
        private ITransaction _transaction;

        public NhSession(ISession session, ITransaction transaction)
        {
            _session = session;
            _transaction = transaction;
        }
        
        public DbConnection Connection
        {
            get
            {
                ThrowIfDisposed();
                return _session.Connection;
            }
        }

        public DbTransaction Transaction
        {
            get
            {
                ThrowIfDisposed();
                return null;
            }
        }

        public IsolationLevel IsolationLevel
        {
            get
            {
                ThrowIfDisposed();
                return Transaction?.IsolationLevel ?? IsolationLevel.Unspecified;
            }
        }

        public Task<IEnumerable<TSource>> QueryAsync<TSource>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<TSource> ExecuteScalarAsync<TSource>(string sql, object param = null, int? commandTimeout = null,
            CommandType? commandType = null, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<dynamic> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
