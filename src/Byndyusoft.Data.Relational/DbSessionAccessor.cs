namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        IDbSession IDbSessionAccessor.DbSession => DbSession.Current;
    }
}