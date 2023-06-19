using System.Text.Json;
using System.Text.Json.Serialization;

namespace Byndyusoft.Data.Relational.TypeHandlers
{
    public static class DefaultJsonTypeHandlerOptions
    {
        /// <summary>
        ///     Reassign if you want to use another JsonSerializerOptions by default
        /// </summary>
        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } =
            JsonTypeHandlerOptionsBase.Instance;
    }
    
    internal static class JsonTypeHandlerOptionsBase
    {
        public static readonly JsonSerializerOptions Instance = new()
        {
            Converters = { new JsonStringEnumConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}