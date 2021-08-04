using System.Collections.Generic;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     This interface describes the method that are required for object deserialization from row.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    public interface ITypeDeserializer<out T>
    {
        /// <summary>
        ///     De-serializes a object from the given row.
        /// </summary>
        /// <param name="row">The row represented as <see cref="IDictionary{K,V}">IDictionary{string,object}</see></param>
        /// <returns>The deserialized element.</returns>
        T Deserialize(IDictionary<string, object> row);
    }
}