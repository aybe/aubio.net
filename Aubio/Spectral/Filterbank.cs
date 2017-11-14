using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/filterbank_8h.html
    /// </summary>
    public sealed class Filterbank : AubioObject
    {
        public Filterbank(int filters, int windowSize)
            : base(Create(filters, windowSize))
        {
        }

        [PublicAPI]
        public FMat Coefficients
        {
            get
            {
                ThrowIfDisposed();

                var handle = NativeMethods.aubio_filterbank_get_coeffs(this);
                var value = new FMat(handle, false);
                return value;
            }
            set
            {
                ThrowIfDisposed();

                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                ThrowIfNot(NativeMethods.aubio_filterbank_set_coeffs(this, value));
            }
        }

        private static IntPtr Create(int filters, int windowSize)
        {
            if (filters <= 0)
                throw new ArgumentOutOfRangeException(nameof(filters));

            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize));

            var handle = NativeMethods.new_aubio_filterbank(filters.ToUInt32(), windowSize.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_filterbank(this);
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_filterbank_do(this, input, output);
        }

        [PublicAPI]
        public bool SetMelCoefficientsSlaney(float sampleRate)
        {
            ThrowIfDisposed();
            var result = NativeMethods.aubio_filterbank_set_mel_coeffs_slaney(this, sampleRate);
            return result;
        }

        [PublicAPI]
        public bool SetTriangularOverlappingBands([NotNull] FVec frequencies, float sampleRate)
        {
            ThrowIfDisposed();

            if (frequencies == null)
                throw new ArgumentNullException(nameof(frequencies));

            var result = NativeMethods.aubio_filterbank_set_triangle_bands(this, frequencies, sampleRate);
            return result;
        }
    }
}