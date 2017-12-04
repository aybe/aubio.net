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

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Sink__* Handle;

        #endregion

        #region Implementation of ISampler

        [PublicAPI]
        public unsafe int SampleRate => (int) aubio_sink_get_samplerate(Handle);

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Sink([NotNull] string uri, int sampleRate = 0)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (sampleRate < 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var handle = new_aubio_sink(uri, (uint) sampleRate);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe int Channels => (int) aubio_sink_get_channels(Handle);

        [PublicAPI]
        public unsafe void Close()
        {
            if (aubio_sink_close(Handle))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec buffer, int write)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (write <= 0)
                throw new ArgumentOutOfRangeException(nameof(write));

            aubio_sink_do(Handle, buffer.Handle, (uint) write);
        }

        [PublicAPI]
        public unsafe void DoMulti([NotNull] FMat buffer, int write)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            if (write <= 0)
                throw new ArgumentOutOfRangeException(nameof(write));

            aubio_sink_do_multi(Handle, buffer.Handle, (uint) write);
        }

        [PublicAPI]
        public unsafe bool PresetChannels(int channels)
        {
            if (channels <= 0)
                throw new ArgumentOutOfRangeException(nameof(channels));

            return !aubio_sink_preset_channels(Handle, (uint) channels);
        }

        [PublicAPI]
        public unsafe bool PresetSampleRate(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            return !aubio_sink_preset_samplerate(Handle, (uint) sampleRate);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_sink(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
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
        private static extern unsafe void del_aubio_sink(
            Sink__* sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sink_close(
            Sink__* sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_sink_do(
            Sink__* sink,
            FVec__* buffer,
            uint write
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_sink_do_multi(
            Sink__* sink,
            FMat__* buffer,
            uint write
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_sink_get_channels(
            Sink__* sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_sink_get_samplerate(
            Sink__* sink
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sink_preset_channels(
            Sink__* sink,
            uint channels
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sink_preset_samplerate(
            Sink__* sink,
            uint sampleRate
        );

        #endregion
    }
}