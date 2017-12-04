using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/onset_8h.html
    /// </summary>
    public sealed class Onset : AubioObject, ISampler
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Onset__* Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _sampleRate;

        #endregion

        #region Implementation of ISampler

        public int SampleRate => _sampleRate;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Onset(OnsetDetection detection, int bufferSize = 1024, int hopSize = 256, int sampleRate = 44100)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            _sampleRate = sampleRate;

            var attribute = detection.GetDescriptionAttribute();
            var method = attribute.Description;

            var handle = new_aubio_onset(method, (uint) bufferSize, (uint) hopSize, (uint) sampleRate);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe bool AdaptiveWhitening
        {
            get => aubio_onset_get_awhitening(Handle);
            set
            {
                if (aubio_onset_set_awhitening(Handle, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe float Compression
        {
            get => aubio_onset_get_compression(Handle);
            set
            {
                if (aubio_onset_set_compression(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe Time Delay
        {
            get
            {
                var samples = aubio_onset_get_delay(Handle);
                var time = Time.FromSamples(_sampleRate, (int) samples);
                return time;
            }
            set
            {
                var samples = value.Samples;

                if (aubio_onset_set_delay(Handle, (uint) samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float Descriptor => aubio_onset_get_descriptor(Handle);

        [PublicAPI]
        public unsafe Time Last
        {
            get
            {
                var samples = aubio_onset_get_last(Handle);
                var time = Time.FromSamples(_sampleRate, (int) samples);
                return time;
            }
        }

        [PublicAPI]
        public unsafe Time MinimumInterOnsetInterval
        {
            get
            {
                var samples = aubio_onset_get_minioi(Handle);
                var time = Time.FromSamples(_sampleRate, (int) samples);
                return time;
            }
            set
            {
                var samples = value.Samples;

                if (aubio_onset_set_minioi(Handle, (uint) samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float Silence
        {
            get => aubio_onset_get_silence(Handle);
            set
            {
                if (aubio_onset_set_silence(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float Threshold
        {
            get => aubio_onset_get_threshold(Handle);
            set
            {
                if (aubio_onset_set_threshold(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float ThresholdedDescriptor => aubio_onset_get_thresholded_descriptor(Handle);

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_onset_do(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void Reset()
        {
            aubio_onset_reset(Handle);
        }

        [PublicAPI]
        public unsafe void SetDefaultParameters(OnsetDetection detection)
        {
            var attribute = detection.GetDescriptionAttribute();
            var description = attribute.Description;

            if (aubio_onset_set_default_parameters(Handle, description))
                throw new ArgumentOutOfRangeException(nameof(detection));
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_onset(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Onset__* new_aubio_onset(
            [MarshalAs(UnmanagedType.LPStr)] string method,
            uint bufferSize,
            uint hopSize,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_onset(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_onset_do(
            Onset__* onset,
            FVec__* input,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_get_awhitening(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_onset_get_compression(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_onset_get_descriptor(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_onset_get_delay(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_onset_get_last(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_onset_get_minioi(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_onset_get_silence(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_onset_get_threshold(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_onset_get_thresholded_descriptor(
            Onset__* onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_awhitening(
            Onset__* onset,
            [MarshalAs(UnmanagedType.Bool)] bool enable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_compression(
            Onset__* onset,
            float lambda
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_default_parameters(
            Onset__* onset,
            [MarshalAs(UnmanagedType.LPStr)] string onsetMode
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_delay(
            Onset__* onset,
            uint delay
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_minioi(
            Onset__* onset,
            uint minioi
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_silence(
            Onset__* onset,
            float silence
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_onset_set_threshold(
            Onset__* onset,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_onset_reset(
            Onset__* onset
        );

        #endregion
    }
}