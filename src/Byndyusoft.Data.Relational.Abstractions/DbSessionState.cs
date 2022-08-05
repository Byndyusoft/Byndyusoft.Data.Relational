namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     The state of the <see cref="IDbSession"/>.
    /// </summary>
    public enum DbSessionState
    {
        Active = 1,

        Committed = 2,

        RolledBack = 4,

        Disposed = 8
    }
}