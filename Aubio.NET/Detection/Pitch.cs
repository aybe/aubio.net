using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    public sealed class Pitch : AubioObject, ISampler
    {
        private readonly int _sampleRate;

        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Pitch__* _pitch;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PitchUnit _unit = PitchUnit.Default;

        #endregion

        #region Implementation of ISampler

        public int SampleRate => _sampleRate;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Pitch(PitchDetection detection, int bufferSize = 1024, int hopSize = 256, int sampleRate = 44100)
        {
            if (bufferSize <= 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (bufferSize < hopSize)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            _sampleRate = sampleRate;

            var attribute = detection.GetDescriptionAttribute();
            var method = attribute.Description;

            var pitch = new_aubio_pitch(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32());
            if (pitch == null)
                throw new ArgumentNullException(nameof(pitch));

            _pitch = pitch;
        }

        [PublicAPI]
        public float Confidence => aubio_pitch_get_confidence(this);

        [PublicAPI]
        public float Silence
        {
            get => aubio_pitch_get_silence(this);
            set
            {
                if (aubio_pitch_set_silence(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Tolerance
        {
            get => aubio_pitch_get_tolerance(this);
            set
            {
                if (aubio_pitch_set_tolerance(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public PitchUnit Unit
        {
            get => _unit;
            set
            {
                var attribute = value.GetDescriptionAttribute();
                var description = attribute.Description;

                if (aubio_pitch_set_unit(this, description))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _unit = value;
            }
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_pitch_do(this, input, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_pitch(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_pitch);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Pitch__* new_aubio_pitch(
            [MarshalAs(UnmanagedType.LPStr)] string method,
            uint bufferSize,
            uint hopSize,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_pitch(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_pitch_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_pitch_get_confidence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_pitch_get_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_pitch_get_tolerance(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_pitch_set_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch,
            float silence
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_pitch_set_tolerance(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch,
            float tolerance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_pitch_set_unit(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch,
            [MarshalAs(UnmanagedType.LPStr)] string pitchUnit
        );

        #endregion
    }
}