using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;

namespace Byndyusoft.Data.Relational.TypeHandlers
{
    public static class JsonTypeHandler
    {
        /// <summary>
        ///     Reassign if you want to use another JsonSerializerOptions by default
        /// </summary>
        public static JsonSerializerOptions DefaultSerializerOptions { get; set; } = new()
        {
            Converters = { new JsonStringEnumConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
    
    public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T?> where T : class
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonTypeHandler()
        {
            _jsonSerializerOptions = JsonTypeHandler.DefaultSerializerOptions;
        }

        public JsonTypeHandler(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }
        
        public override void SetValue(IDbDataParameter parameter, T? value)
        {
            parameter.Value = value == null
                ? DBNull.Value
                : JsonSerializer.Serialize(value, _jsonSerializerOptions);
        }

        public override T? Parse(object value)
        {
            if (value is string json)
                return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

            return null;
        }
    }
}