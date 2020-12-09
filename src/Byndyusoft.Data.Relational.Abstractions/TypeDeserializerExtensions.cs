using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    internal static class TypeDeserializerExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this Task<dynamic> task, ITypeDeserializer<T> deserializer)
        {
            var row = await task.ConfigureAwait(false);
            if (row == null)
                return default;
            return deserializer.Deserialize(row as IDictionary<string, object>);
        }

        public static async Task<IEnumerable<T>> DeserializeAsync<T>(this Task<IEnumerable<dynamic>> task,
            ITypeDeserializer<T> deserializer)
        {
            var rows = await task.ConfigureAwait(false);
            return rows.Select(row => deserializer.Deserialize(row as IDictionary<string, object>));
        }
    }
}