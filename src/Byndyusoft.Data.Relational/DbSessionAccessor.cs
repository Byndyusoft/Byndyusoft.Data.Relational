using System;

namespace Byndyusoft.Data.Relational
{
    using System.Threading;

    public class DbSessionAccessor : IDbSessionAccessor
    {
        // ReSharper disable once InconsistentNaming
        private static readonly AsyncLocal<IDbSession> _session = new AsyncLocal<IDbSession>();

        IDbSession IDbSessionAccessor.DbSession => _session.Value ?? throw new InvalidOperationException("No current session specified");

        internal static IDbSession DbSession
        {
            set => _session.Value = value;
        }
    }
}