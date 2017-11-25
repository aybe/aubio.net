using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/fft_8h.html
    /// </summary>
    public sealed class Fft : AubioObject
    {
        public Fft(int size)
            : base(Create(size))
        {
        }

        private static IntPtr Create(int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            var handle = NativeMethods.new_aubio_fft(size.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_fft(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] CVec spectrum)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            NativeMethods.aubio_fft_do(this, input, spectrum);
        }

        [PublicAPI]
        public void DoComplex([NotNull] FVec input, [NotNull] FVec compspec)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            NativeMethods.aubio_fft_do_complex(this, input, compspec);
        }

        [PublicAPI]
        public void Rdo([NotNull] CVec spectrum, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_fft_rdo(this, spectrum, output);
        }

        [PublicAPI]
        public void RdoComplex([NotNull] FVec compspec, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_fft_rdo_complex(this, compspec, output);
        }

        #region Static

        [PublicAPI]
        public static void GetImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            NativeMethods.aubio_fft_get_imag(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetNorm([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            NativeMethods.aubio_fft_get_norm(compspec, spectrum);
        }

        [PublicAPI]
        public static void GetPhas([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            NativeMethods.aubio_fft_get_phas(compspec, spectrum);
        }

        [PublicAPI]
        public static void GetReal([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            NativeMethods.aubio_fft_get_real(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetRealImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            NativeMethods.aubio_fft_get_realimag(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetSpectrum([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            NativeMethods.aubio_fft_get_spectrum(compspec, spectrum);
        }

        #endregion
    }
}