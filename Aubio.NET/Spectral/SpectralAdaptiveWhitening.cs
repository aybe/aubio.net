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

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe SpectralAdaptiveWhitening__* Handle;

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

            var handle = new_aubio_spectral_whitening((uint) bufferSize, (uint) hopSize, (uint) sampleRate);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe float Floor
        {
            get => aubio_spectral_whitening_get_floor(Handle);
            set
            {
                if (aubio_spectral_whitening_set_floor(Handle, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe float RelaxTime
        {
            get => aubio_spectral_whitening_get_relax_time(Handle);
            set
            {
                if (aubio_spectral_whitening_set_relax_time(Handle, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe void Do([NotNull] CVec fftGrain)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            aubio_spectral_whitening_do(Handle, fftGrain.Handle);
        }

        [PublicAPI]
        public unsafe void Reset()
        {
            aubio_spectral_whitening_reset(Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_spectral_whitening(Handle);
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
        private static extern unsafe void del_aubio_spectral_whitening(
            SpectralAdaptiveWhitening__* whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_spectral_whitening_do(
            SpectralAdaptiveWhitening__* whitening,
            CVec__* fftGrain
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_spectral_whitening_get_floor(
            SpectralAdaptiveWhitening__* whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_spectral_whitening_get_relax_time(
            SpectralAdaptiveWhitening__* whitening
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_spectral_whitening_set_floor(
            SpectralAdaptiveWhitening__* whitening,
            float floor
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_spectral_whitening_set_relax_time(
            SpectralAdaptiveWhitening__* whitening,
            float relaxTime
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_spectral_whitening_reset(
            SpectralAdaptiveWhitening__* whitening
        );

        #endregion
    }
}