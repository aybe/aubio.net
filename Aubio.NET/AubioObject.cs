using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Aubio.NET
{
    public abstract class AubioObject : IDisposable
    {
        internal AubioObject(bool isDisposable = true)
        {
            // make this public object not inheritable

            // handle some special cases
            IsDisposable = isDisposable;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool IsDisposable { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [PublicAPI]
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual void Dispose(bool disposing)
        {
            if (!IsDisposable)
                return; // do not flag as disposed

            if (IsDisposed)
                return;

            DisposeNative();

            if (disposing)
            {
                // nothing
            }

            IsDisposed = true;
        }

        protected abstract void DisposeNative();

        ~AubioObject()
        {
            Dispose(false);
        }

        [PublicAPI]
        protected internal void ThrowIfNot(bool condition, [CanBeNull] string message = null)
        {
            if (!condition)
                return;

            if (string.IsNullOrEmpty(message))
                throw new InvalidOperationException();

            throw new InvalidOperationException(message);
        }

        [PublicAPI]
        internal abstract IntPtr ToPointer();
    }
}