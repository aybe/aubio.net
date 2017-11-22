using System;
using System.Runtime.InteropServices;
using Aubio.NET.Collections;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tempo_8h.html
    /// </summary>
    public sealed class Tempo : AubioObject
    {
        #region Fields

        private readonly unsafe Tempo__* _tempo;

        #endregion

        #region Constructors

        internal unsafe Tempo(Tempo__* tempo)
        {
            if (tempo == null)
                throw new ArgumentNullException(nameof(tempo));

            _tempo = tempo;
        }
 
        [PublicAPI]
        public unsafe Tempo(string method = "default", int bufferSize = 1024, int hopSize = 256, int sampleRate = 44100)
            : this(new_aubio_tempo(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32()))
        {
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public float Bpm
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_bpm(this);
            }
        }

        [PublicAPI]
        public float Confidence
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_confidence(this);
            }
        }

        [PublicAPI]
        public Time Delay
        {
            get
            {
                ThrowIfDisposed();
                var samples = aubio_tempo_get_delay(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
            set
            {
                ThrowIfDisposed();
                aubio_tempo_set_delay(this, value.Samples);
            }
        }

        [PublicAPI]
        public Time LastBeat
        {
            get
            {
                ThrowIfDisposed();
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
                ThrowIfDisposed();
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
                ThrowIfDisposed();
                var samples = aubio_tempo_get_period(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public int SampleRate
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_samplerate(this).ToInt32();
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_silence(this);
            }
            set
            {
                ThrowIfDisposed();
                aubio_tempo_set_silence(this, value);
            }
        }

        [PublicAPI]
        public int TatumSignature
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_tatum_signature(this).ToInt32();
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(aubio_tempo_set_tatum_signature(this, value.ToUInt32()));
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_get_threshold(this);
            }
            set
            {
                ThrowIfDisposed();
                aubio_tempo_set_threshold(this, value);
            }
        }

        [PublicAPI]
        public TempoTatum WasTatum
        {
            get
            {
                ThrowIfDisposed();
                return aubio_tempo_was_tatum(this);
            }
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_tempo_do(this, input, output);
        }

        #endregion

        #region Disposable Members

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
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec output);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_bpm(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_confidence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_tempo_get_last(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_last_tatum(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern int aubio_tempo_get_period(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_tempo_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_tempo_get_tatum_signature(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_tempo_get_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool aubio_tempo_set_delay(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance,
            int delay);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_tatum_signature(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance,
            uint signature);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_silence(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance,
            float silence);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tempo_set_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance,
            float threshold);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern TempoTatum aubio_tempo_was_tatum(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_tempo(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))] Tempo instance);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(TempoMarshaler))]
        private static extern unsafe Tempo__* new_aubio_tempo(
            [MarshalAs(UnmanagedType.LPStr)] string method,
            uint bufferSize,
            uint hopSize,
            uint sampleRate);

        #endregion
    }
}