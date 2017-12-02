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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Filterbank__* _filterbank;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Filterbank(int filters, int windowSize)
        {
            if (filters <= 0)
                throw new ArgumentOutOfRangeException(nameof(filters));

            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize));

            var filterbank = new_aubio_filterbank(filters.ToUInt32(), windowSize.ToUInt32());
            if (filterbank == null)
                throw new ArgumentNullException(nameof(filterbank));

            _filterbank = filterbank;
        }

        [PublicAPI]
        public unsafe FMat Coefficients
        {
            get
            {
                var coeffs = aubio_filterbank_get_coeffs(this);
                var fMat = new FMat(coeffs, false);
                return fMat;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (aubio_filterbank_set_coeffs(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_filterbank_do(this, input, output);
        }

        [PublicAPI]
        public void SetTriangleBands([NotNull] FVec frequencies, float sampleRate)
        {
            if (frequencies == null)
                throw new ArgumentNullException(nameof(frequencies));

            if (aubio_filterbank_set_triangle_bands(this, frequencies, sampleRate))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public void SetMelCoeffsSlaney(float sampleRate)
        {
            if (aubio_filterbank_set_mel_coeffs_slaney(this, sampleRate))
                throw new InvalidOperationException();
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_filterbank(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_filterbank);
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
        private static extern void del_aubio_filterbank(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_filterbank_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FMat__* aubio_filterbank_get_coeffs(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_filterbank_set_coeffs(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat filters
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_filterbank_set_triangle_bands(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec freqs,
            float sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_filterbank_set_mel_coeffs_slaney(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Filterbank filterbank,
            float sampleRate
        );

        #endregion
    }
}