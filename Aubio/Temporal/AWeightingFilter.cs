using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Temporal
{
    public sealed class AWeightingFilter : Filter
    {
        public AWeightingFilter(uint sampleRate)
            : base(Create(sampleRate))
        {
        }

        private static IntPtr Create(uint sampleRate)
        {
            return Create(NativeMethods.new_aubio_filter_a_weighting(sampleRate));
        }

        [PublicAPI]
        public void SetAWeighting(uint sampleRate)
        {
            ThrowIfDisposed();
            ThrowIfNot(NativeMethods.aubio_filter_set_a_weighting(this, sampleRate));
        }
    }
}