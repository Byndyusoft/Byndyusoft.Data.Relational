namespace Byndyusoft.Data.Relational
{
    public interface IDbSessionsIndexer
    {
        /// <summary>
        ///     Gets the current <see cref="IDbSession" /> with specified name.
        ///     Returns null if there is no active <see cref="IDbSession" /> with specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="IDbSession" />.</param>
        /// <returns>Instance of <see cref="IDbSession" />.</returns>
        IDbSession? this[string name] { get; }
    }
}