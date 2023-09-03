using System;
using CommunityToolkit.Diagnostics;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     An object that represents a database query.
    /// </summary>
    /// <see href="https://martinfowler.com/eaaCatalog/queryObject.html" />
    public record QueryObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="QueryObject" /> class.
        /// </summary>
        /// <param name="sql">Database query sql text.</param>
        /// <param name="parameters">Database query parameters.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sql" /> is null or whitespace</exception>
        public QueryObject(string sql, object? parameters = null)
        {
            Guard.IsNotNullOrWhiteSpace(sql, nameof(sql));

            Sql = sql;
            Params = parameters;
        }

        /// <summary>
        ///     Database query sql text.
        /// </summary>
        public string Sql { get; }

        /// <summary>
        ///     Database query parameters.
        /// </summary>
        public object? Params { get; }

        /// <summary>
        ///     Deconstructs the current <see cref="QueryObject" />.
        /// </summary>
        /// <param name="sql">Database query sql text.</param>
        /// <param name="parameters">Database query parameters.</param>
        public void Deconstruct(out string sql, out object? parameters)
        {
            sql = Sql;
            parameters = Params;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="QueryObject" />class.
        /// </summary>
        /// <param name="sql">Database query sql text.</param>
        /// <param name="parameters">Database query parameters.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sql" /> is null or whitespace</exception>
        /// <returns>A new instance of the <see cref="QueryObject" /> class.</returns>
        public static QueryObject Create(string sql, object? parameters = null)
        {
            return new(sql, parameters);
        }

        public static implicit operator QueryObject(string sql) =>
            new(sql);
    }
}