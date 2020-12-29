using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionItems : Dictionary<string, object>, IDisposable, IAsyncDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            foreach (var value in Values)
            {
                if (value is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);

            foreach (var value in Values)
            {
                if (value is IAsyncDisposable asyncDisposable)
                    await asyncDisposable.DisposeAsync();
                else if (value is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}