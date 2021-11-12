using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational.Example
{
    public static class Program
    {
        public static async Task Main()
        {
            var file = "test.db";

            File.Delete(file);
            await File.Create(file).DisposeAsync();

            var serviceProvider =
                new ServiceCollection()
                    .AddRelationalDb(SqliteFactory.Instance, $"data source={file}")
                    .BuildServiceProvider();

            await WriteAsync(serviceProvider);

            await ReadAsync(serviceProvider);
        }

        private static async Task WriteAsync(IServiceProvider serviceProvider)
        {
            var sessionFactory = serviceProvider.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateCommittableSessionAsync();
            await session.ExecuteAsync("CREATE TABLE test (id PRIMARY KEY ASC, name TEXT)");

            await session.ExecuteAsync("INSERT INTO test (name) VALUES ('test1');");
            await session.ExecuteAsync("INSERT INTO test (name) VALUES ('test2');");

            await session.CommitAsync();
        }

        private static async Task ReadAsync(IServiceProvider serviceProvider)
        {
            var sessionFactory = serviceProvider.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateSessionAsync();
            var result = session.Query("SELECT id, name FROM test");
            await foreach (var row in result) Console.WriteLine(JsonConvert.SerializeObject(row));
        }
    }
}