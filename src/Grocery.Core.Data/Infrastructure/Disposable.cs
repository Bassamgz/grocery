namespace Grocery.Core.Data.Infrastructure
{
    using System;

    public class Disposable : IDisposable
    {
        private bool isDisposed;

        ~Disposable()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.DisposeCore();
            }

            this.isDisposed = true;
        }
    }
}
