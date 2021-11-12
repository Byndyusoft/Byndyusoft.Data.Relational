namespace Byndyusoft.Data.Relational
{
    /// <summary>
    /// Options to configure DbSession's support for OpenTelemetry tracing.
    /// Currently no options are available.
    /// </summary>
    public class DbSessionTracingOptions
    {
        public static readonly string ActivitySourceName = nameof(DbSession);
    }
}