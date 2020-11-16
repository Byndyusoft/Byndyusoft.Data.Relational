using System;

namespace Byndyusoft.Data.Relational
{
    public readonly struct QueryObject : IQueryObject
    {
        public QueryObject(string sql, object queryParams = null)
        {
            Sql = !string.IsNullOrEmpty(sql) ? sql : throw new ArgumentNullException(nameof(sql));
            QueryParams = queryParams;
        }

        public string Sql { get; }

        public object QueryParams { get; }
    }
}
