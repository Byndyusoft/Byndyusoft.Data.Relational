using Byndyusoft.Data.Sessions;

namespace Byndyusoft.Data.Relational.Sessions
{
    internal interface IDependentDbSession : IDependentSession
    {
        IDbSession DbSession { get; }
    }
}