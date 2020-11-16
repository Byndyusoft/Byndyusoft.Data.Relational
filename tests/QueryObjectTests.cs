using System;
using Xunit;

namespace Byndyusoft.Data.Relational
{
    public class QueryObjectTests
    {
        [Fact]
        public void Constructow_WithSql()
        {
            // Arrange
            var sql = "SELECT * FROM table";

            // Act
            var queryObject = new QueryObject(sql);

            // Assert
            Assert.Equal(sql, queryObject.Sql);
            Assert.Null(queryObject.QueryParams);
        }

        [Fact]
        public void Constructow_NullSql_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new QueryObject(null));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public void Constructow_WithSqlAndParams()
        {
            // Arrange
            var sql = "SELECT * FROM table";
            var parameters = new object();

            // Act
            var queryObject = new QueryObject(sql, parameters);

            // Assert
            Assert.Equal(sql, queryObject.Sql);
            Assert.Equal(parameters, queryObject.QueryParams);
        }
    }
}
