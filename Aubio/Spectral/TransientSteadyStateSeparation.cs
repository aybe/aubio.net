using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tss_8h.html
    /// </summary>
    public sealed class TransientSteadyStateSeparation : AubioObject
    {
        public TransientSteadyStateSeparation(int bufferSize, int hopSize)
            : base(Create(bufferSize, hopSize))
        {
        }

        private static IntPtr Create(int bufferSize, int hopSize)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = NativeMethods.new_aubio_tss(bufferSize.ToUInt32(), hopSize.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_tss(this);
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] CVec transient, [NotNull] CVec steady)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (transient == null)
                throw new ArgumentNullException(nameof(transient));

            if (steady == null)
                throw new ArgumentNullException(nameof(steady));

            NativeMethods.aubio_tss_do(this, input, transient, steady);
        }

        [PublicAPI]
        public bool SetThreshold(float threshold)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_tss_set_threshold(this, threshold);
            return value;
        }

        [PublicAPI]
        public bool SetAlpha(float alpha)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_tss_set_alpha(this, alpha);
            return value;
        }

        [PublicAPI]
        public bool SetBeta(float beta)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_tss_set_beta(this, beta);
            return value;
        }
    }
}