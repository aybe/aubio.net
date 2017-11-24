using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    public sealed class Pitch : AubioObject
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Pitch__* _pitch;

        #endregion

        #region Constructors

        internal unsafe Pitch([NotNull] Pitch__* pitch)
        {
            if (pitch == null)
                throw new ArgumentNullException(nameof(pitch));

            _pitch = pitch;
        }

        [PublicAPI]
        public unsafe Pitch(PitchMethod method, int bufferSize, int hopSize, int sampleRate)
            : this(new_aubio_pitch2(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32()))
        {
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public float Confidence
        {
            get
            {
                return aubio_pitch_get_confidence(this);
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                return aubio_pitch_get_silence(this);
            }
            set
            {
                ThrowIfNot(aubio_pitch_set_silence(this, value));
            }
        }

        [PublicAPI]
        public float Tolerance
        {
            get
            {
                return aubio_pitch_get_tolerance(this);
            }
            set
            {
                ThrowIfNot(aubio_pitch_set_tolerance(this, value));
            }
        }

        [PublicAPI]
        public PitchUnit Unit
        {
            get
            {
                return aubio_pitch_get_mode(this);
            }
            set
            {
                aubio_pitch_set_mode(this, value);
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

        #region AubioObject Members

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
        private static extern unsafe Pitch__* new_aubio_pitch2(
           [MarshalAs(UnmanagedType.I4)] PitchMethod method,
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
        private static extern PitchUnit aubio_pitch_get_mode(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_pitch_set_mode(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Pitch pitch,
            [MarshalAs(UnmanagedType.I4)] PitchUnit unit
        );

        #endregion
    }
}