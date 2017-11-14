using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/mfcc_8h.html
    /// </summary>
    public sealed class MelFrequencyCepstrumCoefficients : AubioObject
    {
        public MelFrequencyCepstrumCoefficients(int bufferSize, int filters, int coefficients, int sampleRate)
            : base(Create(bufferSize, filters, coefficients, sampleRate))
        {
        }

        private static IntPtr Create(int bufferSize, int filters, int coefficients, int sampleRate)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (filters <= 0)
                throw new ArgumentOutOfRangeException(nameof(filters));

            if (coefficients <= 0)
                throw new ArgumentOutOfRangeException(nameof(coefficients));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var handle = NativeMethods.new_aubio_mfcc(bufferSize.ToUInt32(), filters.ToUInt32(),
                coefficients.ToUInt32(),
                sampleRate.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_mfcc(this);
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_mfcc_do(this, input, output);
        }
    }
}