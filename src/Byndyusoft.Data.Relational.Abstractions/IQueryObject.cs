namespace Byndyusoft.Data.Relational
{
    public interface IQueryObject
    {
        string Sql { get; }

        object QueryParams { get; }
    }
}