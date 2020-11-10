using System;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        // ReSharper disable once InconsistentNaming
        private static readonly AsyncLocal<IDbSession> _session = new AsyncLocal<IDbSession>();

        internal static IDbSession DbSession
        {
            set => _session.Value = value;
        }

        IDbSession IDbSessionAccessor.DbSession =>
            _session.Value ?? throw new InvalidOperationException("No current session specified");
    }
}