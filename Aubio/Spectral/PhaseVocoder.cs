using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/phasevoc_8h.html
    /// </summary>
    public sealed class PhaseVocoder : AubioObject
    {
        public PhaseVocoder(int windowSize, int hopSize) : base(Create(windowSize, hopSize))
        {
        }

        private static IntPtr Create(int windowSize, int hopSize)
        {
            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = NativeMethods.new_aubio_pvoc(windowSize.ToUInt32(), hopSize.ToUInt32());

            return handle;
        }

        [PublicAPI]
        public int HopSize
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_pvoc_get_hop(this);
                return value.ToInt32();
            }
        }

        [PublicAPI]
        public int WindowSize
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_pvoc_get_win(this);
                return value.ToInt32();
            }
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] CVec fftGrain)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            NativeMethods.aubio_pvoc_do(this, input, fftGrain);
        }

        [PublicAPI]
        public void Rdo([NotNull] CVec fftGrain, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_pvoc_rdo(this, fftGrain, output);
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_pvoc(this);
        }
    }
}