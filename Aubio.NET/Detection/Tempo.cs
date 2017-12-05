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

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Tempo__* Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _tatumSignature = 4;

        #endregion

        #region Implementation of ISampler

        public int SampleRate { get; }

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

            SampleRate = sampleRate;

            var handle = new_aubio_tempo("default", (uint) bufferSize, (uint) hopSize, (uint) sampleRate);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe float Bpm => aubio_tempo_get_bpm(Handle);

        [PublicAPI]
        public unsafe float Confidence => aubio_tempo_get_confidence(Handle);

        [PublicAPI]
        public unsafe Time Delay
        {
            get
            {
                var samples = aubio_tempo_get_delay(Handle);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
            set
            {
                if (aubio_tempo_set_delay(Handle, value.Samples))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe Time Last
        {
            get
            {
                var samples = aubio_tempo_get_last(Handle);
                var time = Time.FromSamples(SampleRate, (int) samples);
                return time;
            }
        }

        [PublicAPI]
        public unsafe Time LastTatum
        {
            get
            {
                var samples = aubio_tempo_get_last_tatum(Handle);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public unsafe Time Period
        {
            get
            {
                var samples = aubio_tempo_get_period(Handle);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public unsafe float Silence
        {
            get => aubio_tempo_get_silence(Handle);
            set
            {
                if (aubio_tempo_set_silence(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe int TatumSignature
        {
            get => _tatumSignature;
            set
            {
                if (aubio_tempo_set_tatum_signature(Handle, (uint) value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _tatumSignature = value;
            }
        }

        [PublicAPI]
        public unsafe float Threshold
        {
            get => aubio_tempo_get_threshold(Handle);
            set
            {
                if (aubio_tempo_set_threshold(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe TempoTatum WasTatum => aubio_tempo_was_tatum(Handle);

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_tempo_do(Handle, input.Handle, output.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_tempo(Handle);
        }

        #endregion

        #region Native methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_tempo_do(
            Tempo__* tempo,
            FVec__* input,
            FVec__* output
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_tempo_get_bpm(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_tempo_get_confidence(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int aubio_tempo_get_delay(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_tempo_get_last(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int aubio_tempo_get_last_tatum(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int aubio_tempo_get_period(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_tempo_get_silence(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_tempo_get_samplerate(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_tempo_get_threshold(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tempo_set_delay(
            Tempo__* tempo,
            int delay
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tempo_set_tatum_signature(
            Tempo__* tempo,
            uint signature
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tempo_set_silence(
            Tempo__* tempo,
            float silence
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tempo_set_threshold(
            Tempo__* tempo,
            float threshold
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe TempoTatum aubio_tempo_was_tatum(
            Tempo__* tempo
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_tempo(
            Tempo__* tempo
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