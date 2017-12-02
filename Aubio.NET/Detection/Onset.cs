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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Onset__* _onset;

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

            var onset = new_aubio_onset(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32());
            if (onset == null)
                throw new ArgumentNullException(nameof(onset));

            _onset = onset;
        }

        [PublicAPI]
        public bool AdaptiveWhitening
        {
            get => aubio_onset_get_awhitening(this);
            set
            {
                if (aubio_onset_set_awhitening(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public float Compression
        {
            get => aubio_onset_get_compression(this);
            set
            {
                if (aubio_onset_set_compression(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public Time Delay
        {
            get
            {
                var samples = aubio_onset_get_delay(this);
                var time = Time.FromSamples(_sampleRate, samples.ToInt32());
                return time;
            }
            set
            {
                var samples = value.Samples.ToUInt32();

                if (aubio_onset_set_delay(this, samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Descriptor => aubio_onset_get_descriptor(this);

        [PublicAPI]
        public Time Last
        {
            get
            {
                var samples = aubio_onset_get_last(this);
                var time = Time.FromSamples(_sampleRate, samples.ToInt32());
                return time;
            }
        }

        [PublicAPI]
        public Time MinimumInterOnsetInterval
        {
            get
            {
                var samples = aubio_onset_get_minioi(this);
                var time = Time.FromSamples(_sampleRate, samples.ToInt32());
                return time;
            }
            set
            {
                var samples = value.Samples.ToUInt32();

                if (aubio_onset_set_minioi(this, samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Silence
        {
            get => aubio_onset_get_silence(this);
            set
            {
                if (aubio_onset_set_silence(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get => aubio_onset_get_threshold(this);
            set
            {
                if (aubio_onset_set_threshold(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float ThresholdedDescriptor => aubio_onset_get_thresholded_descriptor(this);

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_onset_do(this, input, output);
        }

        [PublicAPI]
        public void Reset()
        {
            aubio_onset_reset(this);
        }

        [PublicAPI]
        public void SetDefaultParameters(OnsetDetection detection)
        {
            var attribute = detection.GetDescriptionAttribute();
            var description = attribute.Description;

            if (aubio_onset_set_default_parameters(this, description))
                throw new ArgumentOutOfRangeException(nameof(detection));
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_onset(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_onset);
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
        private static extern void del_aubio_onset(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_onset_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_get_awhitening(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_onset_get_compression(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_onset_get_descriptor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_onset_get_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_onset_get_last(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_onset_get_minioi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_onset_get_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_onset_get_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_onset_get_thresholded_descriptor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_awhitening(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            [MarshalAs(UnmanagedType.Bool)] bool enable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_compression(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            float lambda
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_default_parameters(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            [MarshalAs(UnmanagedType.LPStr)] string onsetMode
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            uint delay
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_minioi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            uint minioi
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            float silence
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_onset_set_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_onset_reset(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Onset onset
        );

        #endregion
    }
}