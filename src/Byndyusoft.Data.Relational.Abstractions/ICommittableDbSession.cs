namespace Byndyusoft.Data.Relational
{
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommittableDbSession : IDbSession
    {
        IsolationLevel IsolationLevel { get; }

        void Commit();

#if NETSTANDARD2_1
        ValueTask CommitAsync(CancellationToken cancellationToken = default);
#endif
    }
}