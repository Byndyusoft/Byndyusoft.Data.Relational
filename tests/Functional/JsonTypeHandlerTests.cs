using System;
using System.Data.Common;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Byndyusoft.Data.Relational.TypeHandlers;
using Dapper;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Byndyusoft.Data.Relational.Functional
{
    public class JsonTypeHandlerTests : IAsyncLifetime
    {
        private readonly string _file = $"{Guid.NewGuid()}.db";

        private DbConnection _connection;
        private DbSession _session;
        
        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection($"Data Source={_file}.db;Pooling=false");
            await _connection.ExecuteAsync("CREATE TABLE test (id INT, user TEXT)");

            _session = new DbSession(_connection);
        }

        public async Task DisposeAsync()
        {
            await _session.DisposeAsync();
            await _connection.DisposeAsync();

            File.Delete(_file);
        }
        
        [Fact]
        public async Task SetValue_HandledJsonType_JsonSavedInDB()
        {
            // Arrange
            SqlMapper.AddTypeHandler(new JsonTypeHandler<User>());
            var user = new User
            {
                Name = "Test",
                Login = "Text",
                Age = 90
            };
            var query = new QueryObject("INSERT INTO TEST (id, user) VALUES (1, @user)", new {user});
            
            // Act
            await _session.ExecuteAsync(query);

            // Assert
            var actualRow = await _connection.QuerySingleOrDefaultAsync<string>("SELECT user from test WHERE id = 1");
            var actualUser = JsonSerializer.Deserialize<User>(actualRow);
            
            Assert.Equal(user.Name, actualUser!.Name);
            Assert.Equal(user.Login, actualUser.Login);
            Assert.Equal(user.Age, actualUser.Age);
        }
        
        [Fact]
        public async Task Parse_HandledJsonType_JsonReadFromDB()
        {
            // Arrange
            SqlMapper.AddTypeHandler(new JsonTypeHandler<User>());
            var user = new User
            {
                Name = "Test",
                Login = "Text",
                Age = 90
            };
            var userJson = JsonSerializer.Serialize(user);
            
            await _session.ExecuteAsync($@"INSERT INTO TEST (id, user) VALUES (1, '{userJson}')");
            
            var query = new QueryObject("SELECT user from test WHERE id = 1");

            // Act
            var actualRow = await _connection.QuerySingleOrDefaultAsync<Row>(query.Sql);

            // Assert
            Assert.Equal("Test", actualRow.User.Name);
            Assert.Equal("Text", actualRow.User.Login);
            Assert.Equal(90, actualRow.User.Age);
        }
    }

    class Row
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
    
    class User
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}