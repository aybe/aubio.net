using System;
using System.Diagnostics.CodeAnalysis;

namespace Aubio.Interop
{
    public abstract class Disposable : IDisposable
    {
        protected Disposable(bool isDisposable = true)
        {
            IsDisposable = isDisposable;
        }

        private bool IsDisposable { get; }

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposable)
                return;

            if (IsDisposed)
                return;

            DisposeNative();

            if (disposing)
                DisposeManaged();

            IsDisposed = true;
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeNative()
        {
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}