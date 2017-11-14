using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    public abstract class AubioObject : Disposable
    {
        protected AubioObject(IntPtr handle, bool isDisposable = true)
            : base(isDisposable)
        {
            Handle = handle;
        }

        private IntPtr Handle { get; }

        public static implicit operator IntPtr([NotNull] AubioObject o)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));

            return o.Handle;
        }

        protected static IntPtr Create(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new InvalidOperationException();

            return handle;
        }

        [AssertionMethod]
        protected static void ThrowIfNot(bool condition, string message = null)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }
    }
}