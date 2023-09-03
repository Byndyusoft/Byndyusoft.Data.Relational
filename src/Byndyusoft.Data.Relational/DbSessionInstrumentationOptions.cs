using System.Diagnostics;
using System.Reflection;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Options to configure DbSession's support for OpenTelemetry tracing.
    ///     Currently no options are available.
    /// </summary>
    public class DbSessionInstrumentationOptions
    {
        private static readonly AssemblyName _assemblyName = typeof(DbSession).Assembly.GetName();
        private static readonly string _version = _assemblyName.Version!.ToString();
        public static readonly string ActivitySourceName = _assemblyName.Name!;

        internal static ActivitySource CreateActivitySource()
        {
            return new(ActivitySourceName, _version);
        }
    }
}