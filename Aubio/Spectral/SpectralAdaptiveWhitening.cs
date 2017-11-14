using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/awhitening_8h.html
    /// </summary>
    public sealed class SpectralAdaptiveWhitening : AubioObject
    {
        public SpectralAdaptiveWhitening(int bufferSize, int hopSize, int sampleRate)
            : base(Create(bufferSize, hopSize, sampleRate))
        {
        }

        [PublicAPI]
        public float RelaxTime
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_spectral_whitening_get_relax_time(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_spectral_whitening_set_relax_time(this, value));
            }
        }

        [PublicAPI]
        public float Floor
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_spectral_whitening_get_floor(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_spectral_whitening_set_floor(this, value));
            }
        }

        private static IntPtr Create(int bufferSize, int hopSize, int sampleRate)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var handle = NativeMethods.new_aubio_spectral_whitening(
                bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_spectral_whitening(this);
        }

        [PublicAPI]
        public void Reset()
        {
            ThrowIfDisposed();
            NativeMethods.aubio_spectral_whitening_reset(this);
        }

        [PublicAPI]
        public void Do([NotNull] CVec input)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            NativeMethods.aubio_spectral_whitening_do(this, input);
        }
    }
}