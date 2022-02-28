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

            return DeserializeCore<T>(deserializer, row);
        }

        public static async Task<IEnumerable<T>> DeserializeAsync<T>(this Task<IEnumerable<dynamic>> task,
            ITypeDeserializer<T> deserializer)
        {
            var rows = await task.ConfigureAwait(false);
            return rows.Select(row => (T) DeserializeCore<T>(deserializer, row));
        }

        private static T DeserializeCore<T>(ITypeDeserializer<T> deserializer, dynamic? row)
        {
            if (row == null)
                return default!;
            return deserializer.Deserialize((IDictionary<string, object>) row);
        }
    }
}