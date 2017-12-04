using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    /// <summary>
    ///     https://aubio.org/doc/latest/source_8h.html
    /// </summary>
    public sealed class Source : AubioObject, ISampler
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Source__* Handle;

        #endregion

        #region Implementation of ISampler

        [PublicAPI]
        public unsafe int SampleRate => (int) aubio_source_get_samplerate(Handle);

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Source([NotNull] string uri, int sampleRate = 44100, int hopSize = 256)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (sampleRate < 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = new_aubio_source(uri, (uint) sampleRate, (uint) hopSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe int Channels => (int) aubio_source_get_channels(Handle);

        [PublicAPI]
        public unsafe Time Duration
        {
            get
            {
                var samples = aubio_source_get_duration(Handle);
                var time = Time.FromSamples(SampleRate, (int) samples);
                return time;
            }
        }

        [PublicAPI]
        public unsafe void Close()
        {
            if (aubio_source_close(Handle))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec buffer, out int framesRead)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_source_do(Handle, buffer.Handle, out var read);

            framesRead = (int) read;
        }

        [PublicAPI]
        public unsafe void DoMulti([NotNull] FMat buffer, out int framesRead)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_source_do_multi(Handle, buffer.Handle, out var read);

            framesRead = (int) read;
        }

        [PublicAPI]
        public unsafe void Seek(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            if (aubio_source_seek(Handle, (uint) position))
                throw new ArgumentOutOfRangeException(nameof(position));
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_source(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Source__* new_aubio_source(
            [MarshalAs(UnmanagedType.LPStr)] string uri,
            uint sampleRate,
            uint hopSize
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_source(
            Source__* source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_source_close(
            Source__* source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_source_do(
            Source__* source,
            FVec__* buffer,
            out uint read
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_source_do_multi(
            Source__* source,
            FMat__* buffer,
            out uint read
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_source_get_channels(
            Source__* source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_source_get_duration(
            Source__* source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_source_get_samplerate(
            Source__* source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_source_seek(
            Source__* source,
            uint position
        );

        #endregion
    }
}