using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/awhitening_8h.html
    /// </summary>
    public sealed class SpectralAdaptiveWhitening : AubioObject, ISampler
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe SpectralAdaptiveWhitening__* _whitening;

        #endregion

        #region Implementation of ISampler

        public int SampleRate { get; }

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe SpectralAdaptiveWhitening(int bufferSize = 1024, int hopSize = 256, int sampleRate = 44100)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            SampleRate = sampleRate;

            var whitening = new_aubio_spectral_whitening(
                bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32());
            if (whitening == null)
                throw new ArgumentNullException(nameof(whitening));

            _whitening = whitening;
        }

        [PublicAPI]
        public float Floor
        {
            get => aubio_spectral_whitening_get_floor(this);
            set
            {
                if (aubio_spectral_whitening_set_floor(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public float RelaxTime
        {
            get => aubio_spectral_whitening_get_relax_time(this);
            set
            {
                if (aubio_spectral_whitening_set_relax_time(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public void Do([NotNull] CVec fftGrain)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            aubio_spectral_whitening_do(this, fftGrain);
        }

        [PublicAPI]
        public void Reset()
        {
            aubio_spectral_whitening_reset(this);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_spectral_whitening(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_whitening);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe SpectralAdaptiveWhitening__* new_aubio_spectral_whitening(
            uint bufferSize,
            uint hopSize,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_spectral_whitening(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_spectral_whitening_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec fftGrain
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_spectral_whitening_get_floor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_spectral_whitening_get_relax_time(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_spectral_whitening_set_floor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening,
            float floor
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_spectral_whitening_set_relax_time(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening,
            float relaxTime
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_spectral_whitening_reset(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralAdaptiveWhitening whitening
        );

        #endregion
    }
}