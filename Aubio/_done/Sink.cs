using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.IO
{
    public sealed class Sink : AubioObject
    {
        public Sink(string uri, int sampleRate)
            : base(Create(uri, sampleRate))
        {
        }

        [PublicAPI]
        public int Channels
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_sink_get_channels(this);
                return value.ToInt32();
            }
        }

        [PublicAPI]
        public int SampleRate
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_sink_get_samplerate(this);
                return value.ToInt32();
            }
        }

        private static IntPtr Create([NotNull] string uri, int sampleRate)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var handle = NativeMethods.new_aubio_sink(uri, sampleRate.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_sink(this);
        }

        [PublicAPI]
        public bool Close()
        {
            ThrowIfDisposed();
            var result = NativeMethods.aubio_sink_close(this);
            return result;
        }

        [PublicAPI]
        public int Do([NotNull] FVec buffer)
        {
            ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            NativeMethods.aubio_sink_do(this, buffer, out var written);

            return written.ToInt32();
        }

        [PublicAPI]
        public int DoMulti([NotNull] FMat buffer)
        {
            ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            NativeMethods.aubio_sink_do_multi(this, buffer, out var written);

            return written.ToInt32();
        }

        [PublicAPI]
        public bool PresetChannels(int channels)
        {
            ThrowIfDisposed();

            if (channels <= 0)
                throw new ArgumentOutOfRangeException(nameof(channels));

            var result = NativeMethods.aubio_sink_preset_channels(this, channels.ToUInt32());
            return result;
        }

        [PublicAPI]
        public bool PresetSampleRate(int sampleRate)
        {
            ThrowIfDisposed();

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var result = NativeMethods.aubio_sink_preset_samplerate(this, sampleRate.ToUInt32());
            return result;
        }
    }
}