namespace Byndyusoft.Data.Relational
{
    public interface IDbSessionStorage : IDbSessionsIndexer
    {
        IDbSession? GetCurrentSession(string name);

        void SetCurrentSession(string name, IDbSession? session);

#if NETSTANDARD2_1_OR_GREATER
        IDbSession? IDbSessionsIndexer.this[string name] => GetCurrentSession(name);
#endif
    }
}