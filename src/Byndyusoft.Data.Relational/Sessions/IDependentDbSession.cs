using System;
using Byndyusoft.Data.Relational;
using Byndyusoft.Data.Sessions;

internal interface IDependentDbSession : IDependentSession, IDisposable
{
    IDbSession DbSession { get; }
}