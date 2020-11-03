namespace Byndyusoft.Data.Relational
{
    public interface IDbSessionAccessor
    {
        IDbSession DbSession { get; }
    }
}