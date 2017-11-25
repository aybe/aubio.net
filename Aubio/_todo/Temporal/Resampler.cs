using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Temporal
{
    public sealed class Resampler : AubioObject
    {
        public Resampler(float ratio, uint type)
            : base(Create(ratio, type))
        {
        }

        private static IntPtr Create(float ratio, uint type)
        {
            return Create(NativeMethods.new_aubio_resampler(ratio, type));
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_resampler(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_resampler_do(this, input, output);
        }
    }
}