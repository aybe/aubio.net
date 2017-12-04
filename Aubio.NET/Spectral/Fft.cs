using System;
using System.Diagnostics;
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

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Fft__* Handle;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Fft(int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            var handle = new_aubio_fft((uint) size);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] CVec spectrum)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_do(Handle, input.Handle, spectrum.Handle);
        }

        [PublicAPI]
        public unsafe void DoComplex([NotNull] FVec input, [NotNull] FVec compspec)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_do_complex(Handle, input.Handle, compspec.Handle);
        }

        [PublicAPI]
        public unsafe void Rdo([NotNull] CVec spectrum, [NotNull] FVec output)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_fft_rdo(Handle, spectrum.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void RdoComplex([NotNull] FVec compspec, [NotNull] FVec output)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_fft_rdo_complex(Handle, compspec.Handle, output.Handle);
        }

        [PublicAPI]
        public static unsafe void GetImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_imag(spectrum.Handle, compspec.Handle);
        }

        [PublicAPI]
        public static unsafe void GetNorm([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_norm(compspec.Handle, spectrum.Handle);
        }

        [PublicAPI]
        public static unsafe void GetPhas([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_phas(compspec.Handle, spectrum.Handle);
        }

        [PublicAPI]
        public static unsafe void GetReal([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_real(spectrum.Handle, compspec.Handle);
        }

        [PublicAPI]
        public static unsafe void GetRealImag([NotNull] CVec spectrum, [NotNull] FVec compspec)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            aubio_fft_get_realimag(spectrum.Handle, compspec.Handle);
        }

        [PublicAPI]
        public static unsafe void GetSpectrum([NotNull] FVec compspec, [NotNull] CVec spectrum)
        {
            if (compspec == null)
                throw new ArgumentNullException(nameof(compspec));

            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));

            aubio_fft_get_spectrum(compspec.Handle, spectrum.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_fft(Handle);
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
        private static extern unsafe void del_aubio_fft(
            Fft__* fft
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_do(
            Fft__* fft,
            FVec__* input,
            CVec__* spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_do_complex(
            Fft__* fft,
            FVec__* input,
            FVec__* compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_rdo(
            Fft__* fft,
            CVec__* spectrum,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_rdo_complex(
            Fft__* fft,
            FVec__* compspec,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_imag(
            CVec__* spectrum,
            FVec__* compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_phas(
            FVec__* compspec,
            CVec__* spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_norm(
            FVec__* compspec,
            CVec__* spectrum
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_real(
            CVec__* spectrum,
            FVec__* compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_realimag(
            CVec__* spectrum,
            FVec__* compspec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_fft_get_spectrum(
            FVec__* compspec,
            CVec__* spectrum
        );

        #endregion
    }
}