using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/notes_8h.html
    /// </summary>
    public sealed class Notes : AubioObject
    {
        public Notes(int bufferSize, int hopSize, int sampleRate)
            : base(Create("default", bufferSize, hopSize, sampleRate))
        {
            SampleRate = sampleRate;
        }

        [PublicAPI]
        public int SampleRate { get; }

        [PublicAPI]
        public Time MinimumInterOnsetInterval
        {
            get
            {
                ThrowIfDisposed();
                var milliseconds = NativeMethods.aubio_notes_get_minioi_ms(this);
                var time = Time.FromMilliseconds(SampleRate, milliseconds);
                return time;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_notes_set_minioi_ms(this, value.Milliseconds));
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_notes_get_silence(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_notes_set_silence(this, value));
            }
        }

        private static IntPtr Create([NotNull] string method, int bufferSize, int hopSize, int sampleRate)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var handle = Create(NativeMethods.new_aubio_notes(method, bufferSize.ToUInt32(), hopSize.ToUInt32(),
                sampleRate.ToUInt32()));

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_notes(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_notes_do(this, input, output);
        }
    }
}