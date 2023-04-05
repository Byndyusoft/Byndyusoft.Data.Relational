using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Byndyusoft.Data.Relational.Example
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, string fileName)
        {
            services.AddRelationalDb(SqliteFactory.Instance, $"data source={fileName}");
            //services.AddRelationalDb("sqlite", SqliteFactory.Instance, $"data source={fileName}");

            services.AddOpenTelemetry()
                .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(new AlwaysOnSampler())
                    .SetResourceBuilder(ResourceBuilder.CreateDefault()
                        .AddService(typeof(Startup).Assembly.GetName().Name!))
                    .AddDbSessionInstrumentation()
                    //.AddConsoleExporter()
                    .AddSource(nameof(Program))
                    .AddJaegerExporter(jaeger =>
                    {
                        jaeger.AgentHost = "localhost";
                        jaeger.AgentPort = 6831;
                    });
            });
        }
    }
}