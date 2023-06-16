using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;

namespace Byndyusoft.Data.Relational.TypeHandlers
{
    public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T?> where T : class
    {
        public override void SetValue(IDbDataParameter parameter, T? value)
        {
            parameter.Value = value == null
                ? DBNull.Value
                : JsonSerializer.Serialize(value, JsonTypeHandlerOptions.Instance);
        }

        public override T? Parse(object value)
        {
            if (value is string json)
                return JsonSerializer.Deserialize<T>(json, JsonTypeHandlerOptions.Instance);

            return null;
        }
    }

    internal static class JsonTypeHandlerOptions
    {
        public static readonly JsonSerializerOptions Instance = new()
        {
            Converters = { new JsonStringEnumConverter() },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}