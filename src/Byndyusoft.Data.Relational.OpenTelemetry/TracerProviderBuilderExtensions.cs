using System;
using Byndyusoft.Data.Relational;

// ReSharper disable once CheckNamespace
namespace OpenTelemetry.Trace
{
    /// <summary>
    ///     Extension method for setting up DbSession OpenTelemetry tracing.
    /// </summary>
    public static class TracerProviderBuilderExtensions
    {
        public static TracerProviderBuilder AddDbSessionInstrumentation(
            this TracerProviderBuilder builder,
            Action<DbSessionInstrumentationOptions>? options = null)
        {
            return builder.AddSource(DbSessionInstrumentationOptions.ActivitySourceName);
        }
    }
}