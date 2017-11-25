using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/specdesc_8h.html
    /// </summary>
    public sealed class SpectralDescription : AubioObject
    {
        public SpectralDescription(string method, int bufferSize)
            : base(Create(method, bufferSize))
        {
        }

        private static IntPtr Create([NotNull] string method, int bufferSize)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var handle = NativeMethods.new_aubio_specdesc(method, bufferSize.ToUInt32());

            return handle;
        }

        [PublicAPI]
        public void Do([NotNull] CVec fftGrain, [NotNull] FVec description)
        {
            ThrowIfDisposed();

            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (description == null)
                throw new ArgumentNullException(nameof(description));

            NativeMethods.aubio_specdesc_do(this, fftGrain, description);
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_specdesc(this);
        }
    }
}