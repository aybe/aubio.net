using System;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    public sealed class PhaseVocoder : AubioObject
    {
        #region Fields

        [NotNull]
        private readonly unsafe PhaseVocoder__* _vocoder;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe PhaseVocoder(int windowSize, int hopSize)
        {
            var vocoder = new_aubio_pvoc(windowSize.ToUInt32(), hopSize.ToUInt32());
            if (vocoder == null)
                throw new ArgumentNullException(nameof(vocoder));

            _vocoder = vocoder;
        }

        [PublicAPI]
        public int WindowSize => aubio_pvoc_get_win(this).ToInt32();

        [PublicAPI]
        public int HopSize => aubio_pvoc_get_hop(this).ToInt32();

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] CVec fftGrain)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            aubio_pvoc_do(this, input, fftGrain);
        }

        [PublicAPI]
        public void Rdo([NotNull] CVec fftGrain, [NotNull] FVec output)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_pvoc_rdo(this, fftGrain, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_pvoc(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_vocoder);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe PhaseVocoder__* new_aubio_pvoc(
            uint windowSize,
            uint hopSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_pvoc(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            PhaseVocoder phaseVocoder
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_pvoc_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            PhaseVocoder phaseVocoder,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec fftGrain
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_pvoc_rdo(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            PhaseVocoder phaseVocoder,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec fftGrain,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_pvoc_get_win(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            PhaseVocoder phaseVocoder
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_pvoc_get_hop(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            PhaseVocoder phaseVocoder
        );

        #endregion
    }
}