using System;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    public class DbSessionAccessor : IDbSessionAccessor
    {
        private static readonly AsyncLocal<IDbSession> Session = new AsyncLocal<IDbSession>();

        internal static IDbSession DbSession
        {
            set => Session.Value = value;
        }

        IDbSession IDbSessionAccessor.DbSession =>
            Session.Value ?? throw new InvalidOperationException("No current session specified");
    }
}