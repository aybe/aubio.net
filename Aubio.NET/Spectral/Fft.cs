using System;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/fft_8h.html
    /// </summary>
    public sealed class Fft : AubioObject
    {
        #region Fields

        [NotNull]
        private readonly unsafe Fft__* _fft;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Fft(int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            var fft = new_aubio_fft(size.ToUInt32());
            if (fft == null)
                throw new ArgumentNullException(nameof(fft));

            _fft = fft;
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] CVec spectrum)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_do(this, input, spectrum);
        }

        [PublicAPI]
        public void DoComplex([NotNull] FVec input, [NotNull] FVec compspec)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_do_complex(this, input, compspec);
        }

        [PublicAPI]
        public void Rdo([NotNull] CVec spectrum, [NotNull] FVec output)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_fft_rdo(this, spectrum, output);
        }

        [PublicAPI]
        public void RdoComplex([NotNull] FVec compspec, [NotNull] FVec output)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_fft_rdo_complex(this, compspec, output);
        }

        [PublicAPI]
        public static void GetImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_imag(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetNorm([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_norm(compspec, spectrum);
        }

        [PublicAPI]
        public static void GetPhas([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_phas(compspec, spectrum);
        }

        [PublicAPI]
        public static void GetReal([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_real(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetRealImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_realimag(spectrum, compspec);
        }

        [PublicAPI]
        public static void GetSpectrum([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_spectrum(compspec, spectrum);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_fft(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_fft);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Fft__* new_aubio_fft(
            uint size
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_fft(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Fft fft
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Fft fft,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_do_complex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Fft fft,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_rdo(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Fft fft,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_rdo_complex(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Fft fft,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_imag(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_phas(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_norm(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_real(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_realimag(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_fft_get_spectrum(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec compspec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec spectrum
        );

        #endregion
    }
}