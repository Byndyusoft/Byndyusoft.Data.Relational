using Byndyusoft.Data.Relational;
using System;

// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace
{
    /// <summary>
    /// Extension method for setting up DbSession OpenTelemetry tracing.
    /// </summary>
    public static class TracerProviderBuilderExtensions
    {
        /// <summary>
        /// Subscribes to the DbSession activity source to enable OpenTelemetry tracing.
        /// </summary>
        public static TracerProviderBuilder AddDbSessions(
            this TracerProviderBuilder builder,
            Action<DbSessionTracingOptions>? options = null)
            => builder.AddSource(DbSessionTracingOptions.ActivitySourceName);
    }
}
