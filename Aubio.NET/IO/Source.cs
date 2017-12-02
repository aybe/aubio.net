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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Source__* _source;

        #endregion

        #region Implementation of ISampler

        [PublicAPI]
        public int SampleRate => aubio_source_get_samplerate(this).ToInt32();

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Source([NotNull] string uri, int sampleRate = 44100, int hopSize = 256)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var source = new_aubio_source(uri, sampleRate.ToUInt32(), hopSize.ToUInt32());
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            _source = source;
        }

        [PublicAPI]
        public int Channels => aubio_source_get_channels(this).ToInt32();

        [PublicAPI]
        public Time Duration
        {
            get
            {
                var samples = aubio_source_get_duration(this).ToInt32();
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public void Close()
        {
            if (aubio_source_close(this))
                throw new InvalidOperationException();
        }

        [PublicAPI]
        public void Do([NotNull] FVec buffer, out int framesRead)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_source_do(this, buffer, out var read);

            framesRead = read.ToInt32();
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat buffer, out int framesRead)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_source_do_multi(this, buffer, out var read);

            framesRead = read.ToInt32();
        }

        [PublicAPI]
        public void Seek(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            if (aubio_source_seek(this, position.ToUInt32()))
                throw new ArgumentOutOfRangeException(nameof(position));
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_source(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_source);
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
        private static extern void del_aubio_source(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_source_close(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_source_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec buffer,
            out uint read
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_source_do_multi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat buffer,
            out uint read
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_channels(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_duration(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_source_seek(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Source source,
            uint position
        );

        #endregion
    }
}