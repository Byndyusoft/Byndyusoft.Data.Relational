using System;
using Xunit;

namespace Byndyusoft.Data.Relational.Unit
{
    public class QueryObjectTests
    {
        [Fact]
        public void Constructor_WithSql()
        {
            // Arrange
            var sql = "SELECT * FROM table";

            // Act
            var queryObject = new QueryObject(sql);

            // Assert
            Assert.Equal(sql, queryObject.Sql);
            Assert.Null(queryObject.Params);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new QueryObject(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }

        [Fact]
        public void Constructor_WithSqlAndParams()
        {
            // Arrange
            var sql = "SELECT * FROM table";
            var parameters = new object();

            // Act
            var queryObject = new QueryObject(sql, parameters);

            // Assert
            Assert.Equal(sql, queryObject.Sql);
            Assert.Equal(parameters, queryObject.Params);
        }

        [Fact]
        public void Deconstruct_Test()
        {
            // Arrange
            var queryObject = new QueryObject("SELECT * FROM table", new object());

            // Act
            var (sql, parameters) = queryObject;

            // Assert
            Assert.Equal(queryObject.Sql, sql);
            Assert.Equal(queryObject.Params, parameters);
        }

        [Fact]
        public void Create_Test()
        {
            // Arrange
            var sql = "SELECT * FROM table";
            var parameters = new object();

            // Act
            var queryObject = QueryObject.Create(sql, parameters);

            // Assert
            Assert.Equal(sql, queryObject.Sql);
            Assert.Equal(parameters, queryObject.Params);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_NullSql_ThrowsException(string sql)
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => QueryObject.Create(sql));

            // Assert
            Assert.Equal("sql", exception.ParamName);
        }
    }
}