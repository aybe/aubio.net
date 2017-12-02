using System;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    public sealed class Mfcc : AubioObject
    {
        #region Fields

        [NotNull]
        private readonly unsafe Mfcc__* _mfcc;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Mfcc(int bufferSize, int filters, int coefficients, int sampleRate)
        {
            var mfcc = new_aubio_mfcc(
                bufferSize.ToUInt32(), filters.ToUInt32(), coefficients.ToUInt32(), sampleRate.ToUInt32());
            if (mfcc == null)
                throw new ArgumentNullException(nameof(mfcc));

            _mfcc = mfcc;
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] CVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_mfcc_do(this, input, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_mfcc(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_mfcc);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Mfcc__* new_aubio_mfcc(
            uint bufferSize,
            uint filters,
            uint coefficients,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_mfcc(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Mfcc mfcc
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_mfcc_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Mfcc mfcc,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec output
        );

        #endregion
    }
}