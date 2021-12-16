namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Provides access to the current <see cref="IDbSession" />, if one is available.
    /// </summary>
    public interface IDbSessionAccessor
    {
        /// <summary>
        ///     Gets the current <see cref="IDbSession" />. Returns null if there is no active <see cref="IDbSession" />.
        /// </summary>
        IDbSession? DbSession { get; }
    }
}