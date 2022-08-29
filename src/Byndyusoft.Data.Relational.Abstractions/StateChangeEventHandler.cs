namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Provides data for the finished event of an <see cref="IDbSession" />.
    /// </summary>
    public class DbSessionFinishedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DbSessionFinishedEventArgs" /> class,
        /// </summary>
        public DbSessionFinishedEventArgs(DbSessionState sessionState)
        {
            SessionState = sessionState;
        }

        /// <summary>
        ///     Gets the state of the session.
        /// </summary>
        public DbSessionState SessionState { get; }
    }
}