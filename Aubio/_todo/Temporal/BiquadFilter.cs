using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Temporal
{
    public sealed class BiquadFilter : Filter
    {
        public BiquadFilter(double b0, double b1, double b2, double a1, double a2)
            : base(Create(b0, b1, b2, a1, a2))
        {
        }

        private static IntPtr Create(double b0, double b1, double b2, double a1, double a2)
        {
            return Create(NativeMethods.new_aubio_filter_biquad(b0, b1, b2, a1, a2));
        }

        [PublicAPI]
        public void SetBiquad(double b0, double b1, double b2, double a1, double a2)
        {
            ThrowIfDisposed();
            ThrowIfNot(NativeMethods.aubio_filter_set_biquad(this, b0, b1, b2, a1, a2));
        }
    }
}