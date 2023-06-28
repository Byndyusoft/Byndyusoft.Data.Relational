using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Byndyusoft.Data.Relational.Example
{
    public static class Program
    {
        private const string FileName = "test.db";
        private static readonly ActivitySource Activity = new(nameof(Program));

        static Program()
        {
            System.Diagnostics.Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        }

        public static async Task Main(string[] args)
        {
            File.Delete(FileName);
            await File.Create(FileName).DisposeAsync();

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => new Startup().ConfigureServices(services, FileName))
                .Build();

            await host.StartAsync();

            using (var activity = Activity.StartActivity())
            {
                try
                {
                    await WriteAsync(host.Services);
                    await ReadAsync(host.Services);
                }
                catch (Exception)
                {
                    activity?.SetTag("error", true);
                    activity?.SetStatus(ActivityStatusCode.Error);
                }
            }

            await host.StopAsync();
        }

        private static async Task WriteAsync(IServiceProvider services)
        {
            var sessionFactory = services.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateCommittableSessionAsync();
            await session.ExecuteAsync("CREATE TABLE test (id PRIMARY KEY ASC, name TEXT)");

            await session.ExecuteAsync("INSERT INTO test (name) VALUES ('test1');");
            await session.ExecuteAsync("INSERT INTO test (name) VALUES ('test2');");

            await session.CommitAsync();
        }

        private static async Task ReadAsync(IServiceProvider services)
        {
            var sessionFactory = services.GetRequiredService<IDbSessionFactory>();
            await using var session = await sessionFactory.CreateSessionAsync();
            var result = session.Query("SELECT id, name FROM test");
            await foreach (var row in result) Console.WriteLine(JsonConvert.SerializeObject(row));
        }
    }
}