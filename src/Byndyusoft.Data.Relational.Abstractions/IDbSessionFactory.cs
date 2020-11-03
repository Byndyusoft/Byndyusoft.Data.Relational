namespace Byndyusoft.Data.Relational
{
    using System.Data;
    using System.Threading.Tasks;

    public interface IDbSessionFactory
    {
        Task<IDbSession> CreateSessionAsync();

        Task<ICommittableDbSession> CreateSessionAsync(IsolationLevel isolationLevel);
    }
}