namespace Byndyusoft.Data.Relational
{
    public static class DbSessionEvents
    {
        public static readonly string Start = nameof(Start);
        public static readonly string Commit = nameof(Commit);
        public static readonly string Rollback = nameof(Rollback);
        public static readonly string Dispose = nameof(Dispose);
    }
}