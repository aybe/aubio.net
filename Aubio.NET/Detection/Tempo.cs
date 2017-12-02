using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tempo_8h.html
    /// </summary>
    public sealed class Tempo : AubioObject, ISampler
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Tempo__* _tempo;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _tatumSignature = 4;

        #endregion

        #region Implementation of ISampler

        public int SampleRate => aubio_tempo_get_samplerate(this).ToInt32();

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Tempo(int bufferSize = 1024, int hopSize = 256, int sampleRate = 44100)
        {
            if (bufferSize <= 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (bufferSize < hopSize)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var tempo = new_aubio_tempo("default", bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32());
            if (tempo == null)
                throw new ArgumentNullException(nameof(tempo));

            _tempo = tempo;
        }

        [PublicAPI]
        public float Bpm => aubio_tempo_get_bpm(this);

        [PublicAPI]
        public float Confidence => aubio_tempo_get_confidence(this);

        [PublicAPI]
        public Time Delay
        {
            get
            {
                var samples = aubio_tempo_get_delay(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
            set
            {
                if (aubio_tempo_set_delay(this, value.Samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public Time Last
        {
            get
            {
                var samples = aubio_tempo_get_last(this).ToInt32();
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public Time LastTatum
        {
            get
            {
                var samples = aubio_tempo_get_last_tatum(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public Time Period
        {
            get
            {
                var samples = aubio_tempo_get_period(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public float Silence
        {
            get => aubio_tempo_get_silence(this);
            set
            {
                if (aubio_tempo_set_silence(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public int TatumSignature
        {
            get => _tatumSignature;
            set
            {
                if (aubio_tempo_set_tatum_signature(this, value.ToUInt32()))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _tatumSignature = value;
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get => aubio_tempo_get_threshold(this);
            set
            {
                if (aubio_tempo_set_threshold(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public TempoTatum WasTatum => aubio_tempo_was_tatum(this);

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_tempo_do(this, input, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_tempo(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_tempo);
        }

        #endregion

        #region Native methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_tempo_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_bpm(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_confidence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_tempo_get_last(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_last_tatum(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_period(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_tempo_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo,
            int delay
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_tatum_signature(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo,
            uint signature
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo,
            float silence
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo,
            float threshold
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern TempoTatum aubio_tempo_was_tatum(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_tempo(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Tempo tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Tempo__* new_aubio_tempo(
            [MarshalAs(UnmanagedType.LPStr)] string method,
            uint bufferSize,
            uint hopSize,
            uint sampleRate
        );

        #endregion
    }
}