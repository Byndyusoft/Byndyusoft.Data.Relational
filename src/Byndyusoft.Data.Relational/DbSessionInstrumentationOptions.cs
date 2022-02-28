using System.Diagnostics;
using System.Reflection;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    /// Options to configure DbSession's support for OpenTelemetry tracing.
    /// Currently no options are available.
    /// </summary>
    public class DbSessionInstrumentationOptions
    {
        private static readonly AssemblyName AssemblyName = typeof(DbSession).Assembly.GetName();
        private static readonly string Version = AssemblyName.Version!.ToString();
        public static readonly string ActivitySourceName = AssemblyName.Name;

        internal static ActivitySource CreateActivitySource() => new ActivitySource(ActivitySourceName, Version);
    }
}