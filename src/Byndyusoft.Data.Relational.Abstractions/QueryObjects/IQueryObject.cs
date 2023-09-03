// ReSharper disable once CheckNamespace
namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     An object that represents a database query.
    /// </summary>
    /// <see href="https://martinfowler.com/eaaCatalog/queryObject.html" />
    public interface IQueryObject
    {
        /// <summary>
        ///     Database query sql text.
        /// </summary>
        string Sql { get; }

        /// <summary>
        ///     Database query parameters.
        /// </summary>
        object? Params { get; }
    }
}