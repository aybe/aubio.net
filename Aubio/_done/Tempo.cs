using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tempo_8h.html
    /// </summary>
    public sealed class Tempo : AubioObject
    {
        [PublicAPI]
        public enum Tatum : uint
        {
            None = 0,
            Tatum = 1,
            Beat = 2
        }

        public Tempo(int bufferSize, int hopSize, int sampleRate)
            : base(Create("default", bufferSize, hopSize, sampleRate))
        {
            SampleRate = sampleRate;
        }
        [PublicAPI]
        public int SampleRate { get; }

        [PublicAPI]
        public float Bpm
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_tempo_get_bpm(this);
                return value;
            }
        }

        [PublicAPI]
        public float Confidence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_tempo_get_confidence(this);
                return value;
            }
        }

        [PublicAPI]
        public Time Delay
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_tempo_get_delay(this);
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_tempo_set_delay(this, value.Samples));
            }
        }

        [PublicAPI]
        public Time Last
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_tempo_get_last(this);
                var time = Time.FromSamples(SampleRate, samples.ToInt32());
                return time;
            }
        }

        [PublicAPI]
        public float LastTatum
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_tempo_get_last_tatum(this);
                return value;
            }
        }

        [PublicAPI]
        public Time Period
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_tempo_get_period(this);
                var time = Time.FromSamples(SampleRate, samples.ToInt32());
                return time;
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_tempo_get_silence(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_tempo_set_silence(this, value));
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_tempo_get_threshold(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_tempo_set_threshold(this, value));
            }
        }

        [PublicAPI]
        public Tatum WasTatum
        {
            get
            {
                ThrowIfDisposed();
                return NativeMethods.aubio_tempo_was_tatum(this);
            }
        }

        private static IntPtr Create([NotNull] string method, int bufferSize, int hopSize, int sampleRate)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            var handle = Create(NativeMethods.new_aubio_tempo(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32()));
            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_tempo(this);
        }

        [PublicAPI]
        public void Do(FVec input, FVec tempo)
        {
            ThrowIfDisposed();
            NativeMethods.aubio_tempo_do(this, input, tempo);
        }

        [PublicAPI]
        public void SetTatumSignature(uint signature)
        {
            ThrowIfDisposed();
            ThrowIfNot(NativeMethods.aubio_tempo_set_tatum_signature(this, signature));
        }
    }
}