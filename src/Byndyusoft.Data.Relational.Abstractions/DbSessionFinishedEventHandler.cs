namespace Byndyusoft.Data.Relational
{
    /// <summary>
    ///     Represents the method that will handle the <see cref="IDbSession.Finished"/> event.
    /// </summary>
    public delegate void DbSessionFinishedEventHandler(object sender, DbSessionFinishedEventArgs e);
}