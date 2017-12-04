using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/filterbank_8h.html
    ///     https://aubio.org/doc/latest/filterbank__mel_8h.html
    /// </summary>
    public sealed class Filterbank : AubioObject
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Filterbank__* Handle;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Filterbank(int filters, int windowSize)
        {
            if (filters <= 0)
                throw new ArgumentOutOfRangeException(nameof(filters));

            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize));

            var handle = new_aubio_filterbank((uint) filters, (uint) windowSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe FMat Coefficients
        {
            get
            {
                var coeffs = aubio_filterbank_get_coeffs(Handle);
                var fMat = new FMat(coeffs, false);
                return fMat;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (aubio_filterbank_set_coeffs(Handle, value.Handle))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe void Do([NotNull] CVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_filterbank_do(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void SetTriangleBands([NotNull] FVec frequencies, float sampleRate)
        {
            if (frequencies == null)
                throw new ArgumentNullException(nameof(frequencies));

            if (aubio_filterbank_set_triangle_bands(Handle, frequencies.Handle, sampleRate))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public unsafe void SetMelCoeffsSlaney(float sampleRate)
        {
            if (aubio_filterbank_set_mel_coeffs_slaney(Handle, sampleRate))
                throw new InvalidOperationException();
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_filterbank(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Filterbank__* new_aubio_filterbank(
            uint filters,
            uint windowSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_filterbank(
            Filterbank__* filterbank
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_filterbank_do(
            Filterbank__* filterbank,
            CVec__* input,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FMat__* aubio_filterbank_get_coeffs(
            Filterbank__* filterbank
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_filterbank_set_coeffs(
            Filterbank__* filterbank,
            FMat__* filters
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_filterbank_set_triangle_bands(
            Filterbank__* filterbank,
            FVec__* freqs,
            float sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_filterbank_set_mel_coeffs_slaney(
            Filterbank__* filterbank,
            float sampleRate
        );

        #endregion
    }
}