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
        DbTransaction? Transaction { get; }

        /// <summary>
        ///     Gets a collection of key/value pairs that provide additional information about the <see cref="IDbSession" />.
        /// </summary>
        IDictionary<string, object> Items { get; }

        /// <summary>
        ///     Gets a string that describes the state of the <see cref="IDbSession" />.
        /// </summary>
        DbSessionState State { get; }

        /// <summary>
        ///     Occurs when the session becomes finished.
        /// </summary>
        event DbSessionFinishedEventHandler Finished;
    }
}