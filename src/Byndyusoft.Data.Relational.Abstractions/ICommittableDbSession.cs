namespace Byndyusoft.Data.Relational
{
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommittableDbSession : IDbSession
    {
        IsolationLevel IsolationLevel { get; }

        void Commit();

        void Rollback();

#if NETSTANDARD2_1
        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);
#endif
    }
}