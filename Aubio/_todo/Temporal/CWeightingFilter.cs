using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Temporal
{
    public sealed class CWeightingFilter : Filter
    {
        public CWeightingFilter(uint sampleRate)
            : base(Create(sampleRate))
        {
        }

        private static IntPtr Create(uint sampleRate)
        {
            return Create(NativeMethods.new_aubio_filter_c_weighting(sampleRate));
        }

        [PublicAPI]
        public void SetCWeighting(uint sampleRate)
        {
            ThrowIfDisposed();
            ThrowIfNot(NativeMethods.aubio_filter_set_c_weighting(this, sampleRate));
        }
    }
}