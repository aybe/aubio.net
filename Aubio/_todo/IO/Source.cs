using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.IO
{
    /// <summary>
    /// https://aubio.org/doc/latest/source_8h.html
    /// </summary>
    public sealed class Source : AubioObject
    {
        public Source(int sampleRate, int hopSize)
            : base(Create(sampleRate, hopSize))
        {
        }

        [PublicAPI]
        public int Channels
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_source_get_channels(this);
                return value.ToInt32();
            }
        }

        [PublicAPI]
        public Time Duration
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_source_get_duration(this);
                var time = Time.FromSamples(SampleRate, value.ToInt32());
                return time;
            }
        }

        [PublicAPI]
        public int SampleRate
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_source_get_samplerate(this);
                return value.ToInt32();
            }
        }

        private static IntPtr Create(int sampleRate, int hopSize)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = NativeMethods.new_aubio_source(sampleRate.ToUInt32(), hopSize.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_source(this);
        }

        [PublicAPI]
        public bool Close()
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_source_close(this);
            return value;
        }

        [PublicAPI]
        public int Do([NotNull] FVec buffer)
        {
            ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            NativeMethods.aubio_source_do(this, buffer, out var read);

            return read.ToInt32();
        }

        [PublicAPI]
        public int DoMulti([NotNull] FMat buffer)
        {
            ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            NativeMethods.aubio_source_do_multi(this, buffer, out var read);

            return read.ToInt32();
        }

        [PublicAPI]
        public bool Seek(int position)
        {
            ThrowIfDisposed();

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            var result = NativeMethods.aubio_source_seek(this, position.ToUInt32());
            return result;
        }
    }
}