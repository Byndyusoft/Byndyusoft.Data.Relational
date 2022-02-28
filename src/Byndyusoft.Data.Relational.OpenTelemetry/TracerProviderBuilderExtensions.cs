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
        /// <summary>
        ///     Subscribes to the DbSession activity source to enable OpenTelemetry tracing.
        /// </summary>
        [Obsolete("Use AddDbSessionInstrumentation")]
        public static TracerProviderBuilder AddDbSessions(
            this TracerProviderBuilder builder,
            Action<DbSessionInstrumentationOptions>? options = null)
        {
            return AddDbSessionInstrumentation(builder, options);
        }

        public static TracerProviderBuilder AddDbSessionInstrumentation(
            this TracerProviderBuilder builder,
            Action<DbSessionInstrumentationOptions>? options = null)
        {
            return builder.AddSource(DbSessionInstrumentationOptions.ActivitySourceName);
        }
    }
}