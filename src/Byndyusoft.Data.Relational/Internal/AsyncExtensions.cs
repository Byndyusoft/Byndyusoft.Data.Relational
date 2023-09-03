#if NETSTANDARD2_0

using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System.Data.Common
{
    internal static class AsyncExtensions
    {
        public static ValueTask<DbTransaction> BeginTransactionAsync(this DbConnection connection,
            IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var transaction = connection.BeginTransaction(isolationLevel);
            return new ValueTask<DbTransaction>(transaction);
        }

        public static ValueTask DisposeAsync(this DbConnection connection)
        {
            connection.Dispose();
            return new ValueTask();
        }

        public static ValueTask DisposeAsync(this DbTransaction transaction)
        {
            transaction.Dispose();
            return new ValueTask();
        }

        public static ValueTask CommitAsync(this DbTransaction transaction,
            CancellationToken _ = default)
        {
            transaction.Commit();
            return new ValueTask();
        }

        public static ValueTask RollbackAsync(this DbTransaction transaction,
            CancellationToken _ = default)
        {
            transaction.Rollback();
            return new ValueTask();
        }
    }
}

#endif