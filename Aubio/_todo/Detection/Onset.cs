using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/onset_8h.html
    /// </summary>
    public sealed class Onset : AubioObject
    {
        public Onset(string method, int bufferSize, int hopSize, int sampleRate)
            : base(Create(method, bufferSize, hopSize, sampleRate))
        {
            SampleRate = sampleRate;
        }
        [PublicAPI]
        public int SampleRate { get; }

        [PublicAPI]
        public bool AdaptiveWhitening
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_awhitening(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_awhitening(this, value));
            }
        }

        [PublicAPI]
        public float Compression
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_compression(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_compression(this, value));
            }
        }

        [PublicAPI]
        public Time Delay
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_onset_get_delay(this);
                var time = Time.FromSamples(SampleRate, samples.ToInt32());
                return time;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_delay(this, value.Samples.ToUInt32()));
            }
        }

        [PublicAPI]
        public float Descriptor
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_descriptor(this);
                return value;
            }
        }

        [PublicAPI]
        public Time Last
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_onset_get_last(this);
                var time = Time.FromSamples(SampleRate, samples.ToInt32());
                return time;
            }
        }

        [PublicAPI]
        public Time MinimumInterOnsetInterval
        {
            get
            {
                ThrowIfDisposed();
                var samples = NativeMethods.aubio_onset_get_minioi(this);
                var time = Time.FromSamples(SampleRate, samples.ToInt32());
                return time;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_minioi(this, value.Samples.ToUInt32()));
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_silence(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_silence(this, value));
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_threshold(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_onset_set_threshold(this, value));
            }
        }

        [PublicAPI]
        public float ThresholdedDescriptor
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_onset_get_thresholded_descriptor(this);
                return value;
            }
        }

        private static IntPtr Create([NotNull] string method, int bufferSize, int hopSize, int sampleRate)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            var handle = Create(NativeMethods.new_aubio_onset(method, bufferSize.ToUInt32(), hopSize.ToUInt32(), sampleRate.ToUInt32()));

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_onset(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec onset)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (onset == null)
                throw new ArgumentNullException(nameof(onset));

            NativeMethods.aubio_onset_do(this, input, onset);
        }

        [PublicAPI]
        public void Reset()
        {
            ThrowIfDisposed();
            NativeMethods.aubio_onset_reset(this);
        }

        [PublicAPI]
        public void SetDefaultParameters([NotNull] string mode)
        {
            ThrowIfDisposed();

            if (mode == null)
                throw new ArgumentNullException(nameof(mode));

            ThrowIfNot(NativeMethods.aubio_onset_set_default_parameters(this, mode));
        }
    }
}