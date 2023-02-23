using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Sessions;

public class DbSessionsIndexer : IDbSessionsIndexer
{
    private readonly IDbSessionFactory _sessionFactory;
    private readonly ISessionStorage _sessionStorage;

    public DbSessionsIndexer(
        IDbSessionFactory sessionFactory, 
        ISessionStorage sessionStorage)
    {
        _sessionFactory = sessionFactory;
        _sessionStorage = sessionStorage;
    }

    public IDbSession? this[string name]
    {
        get
        {
            var key = $"Byndyusoft.Data.Relational.{name}";

            var session = _sessionStorage.GetCurrent();
            if (session is null)
                return null;

            if (session.DependentSessions.TryGetValue(key, out var dependentDbSession) == false)
            {
                if (session is ICommitableSession commitableSession)
                {
                    var dbSession = _sessionFactory.CreateCommittableSessionAsync(commitableSession.IsolationLevel)
                        .GetAwaiter().GetResult();
                    dependentDbSession = new DependentCommitableDbSession(dbSession);
                }
                else
                {
                    var dbSession = _sessionFactory.CreateSessionAsync()
                        .GetAwaiter().GetResult();
                    dependentDbSession = new DependentDbSession(dbSession);
                }

                session.Enlist(key, dependentDbSession);
            }

            return ((IDependentDbSession) dependentDbSession).DbSession;
        }
    }
}