using System;
using System.Threading;

namespace Byndyusoft.Data.Relational
{
    public partial class DbSession
    {
        // ReSharper disable once InconsistentNaming
        private static readonly AsyncLocal<Wrapper?> _current = new AsyncLocal<Wrapper?>();

        internal static DbSession? Current
        {
            //private set
            set
            {
                var wrapper = _current.Value ??= new Wrapper();
                if (value != null && wrapper.Value != null)
                    throw new InvalidOperationException($"{nameof(DbSession)} already exists");

                wrapper.Value = value;
            }
            get => _current.Value?.Value;
        }

        private class Wrapper
        {
            public DbSession? Value { get; set; }
        }
    }
}