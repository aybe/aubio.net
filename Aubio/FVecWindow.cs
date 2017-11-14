using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    public sealed class FVecWindow : FVec
    {
        public FVecWindow(FVecWindowType windowType, int size)
            : base(Create(windowType, size))
        {
        }

        private static IntPtr Create(FVecWindowType windowType, int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            var handle = NativeMethods.new_aubio_window(windowType.GetDescription(), size.ToUInt32());
            return handle;
        }

        [PublicAPI]
        public void SetWindowType(FVecWindowType windowType)
        {
            ThrowIfDisposed();
            ThrowIfNot(NativeMethods.fvec_set_window(this, windowType.GetDescription()));
        }
    }
}