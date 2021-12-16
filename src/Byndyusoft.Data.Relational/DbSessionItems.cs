using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Byndyusoft.Data.Relational
{
    internal class DbSessionItems : Dictionary<string, object>, IDisposable, IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);

            foreach (var value in Values)
                switch (value)
                {
                    case IAsyncDisposable asyncDisposable:
                        await asyncDisposable.DisposeAsync().ConfigureAwait(false);
                        break;
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            foreach (var value in Values)
                if (value is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}