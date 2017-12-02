using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    /// <summary>
    ///     https://aubio.org/doc/latest/sink_8h.html
    /// </summary>
    public sealed class Sink : AubioObject, ISampler
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Sink__* _sink;

        #endregion

        #region Implementation of ISampler

        [PublicAPI]
        public int SampleRate => aubio_sink_get_samplerate(this).ToInt32();

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Sink([NotNull] string uri, int sampleRate = 0)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (sampleRate < 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var sink = new_aubio_sink(uri, sampleRate.ToUInt32());
            if (sink == null)
                throw new ArgumentNullException(nameof(sink));

            _sink = sink;
        }

        [PublicAPI]
        public int Channels => aubio_sink_get_channels(this).ToInt32();

        [PublicAPI]
        public void Close()
        {
            if (aubio_sink_close(this))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public void Do([NotNull] FVec buffer, int write)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (write <= 0)
                throw new ArgumentOutOfRangeException(nameof(write));

            aubio_sink_do(this, buffer, write.ToUInt32());
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat buffer, int write)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (write <= 0)
                throw new ArgumentOutOfRangeException(nameof(write));

            aubio_sink_do_multi(this, buffer, write.ToUInt32());
        }

        [PublicAPI]
        public bool PresetChannels(int channels)
        {
            if (channels <= 0)
                throw new ArgumentOutOfRangeException(nameof(channels));

            return !aubio_sink_preset_channels(this, channels.ToUInt32());
        }

        [PublicAPI]
        public bool PresetSampleRate(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            return !aubio_sink_preset_samplerate(this, sampleRate.ToUInt32());
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_sink(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_sink);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Sink__* new_aubio_sink(
            [MarshalAs(UnmanagedType.LPStr)] string uri,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_sink(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sink_close(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_sink_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec buffer,
            uint write
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_sink_do_multi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat buffer,
            uint write
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_sink_get_channels(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_sink_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sink_preset_channels(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink,
            uint channels
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sink_preset_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sink sink,
            uint sampleRate
        );

        #endregion
    }
}