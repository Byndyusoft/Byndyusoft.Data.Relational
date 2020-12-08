using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Represents a database session - object that aggregates connection and transaction.
    /// </summary>
    public interface IDbSession : IDisposable, IAsyncDisposable
    {
        /// <summary>
        ///     Gets the <see cref="DbConnection" /> used by this <see cref="IDbSession" />.
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        ///     Gets the <see cref="DbTransaction" /> used by this <see cref="IDbSession" />.
        /// </summary>
        DbTransaction Transaction { get; }

        /// <summary>
        ///     Gets a collection of key/value pairs that provide additional information about the <see cref="IDbSession" />.
        /// </summary>
        IDictionary<string, object> Data { get; }
    }
}