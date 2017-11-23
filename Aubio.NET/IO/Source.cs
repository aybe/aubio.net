using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Aubio.NET.Collections;
using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    public sealed class Source : AubioObject
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Source__* _source;

        #endregion

        #region Constructors

        internal unsafe Source([NotNull] Source__* source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            _source = source;
        }
        
        [PublicAPI]
        public unsafe Source(string uri, int sampleRate, int hopSize)
            : this(new_aubio_source(uri, sampleRate.ToUInt32(), hopSize.ToUInt32()))
        {
        }

        #endregion

        #region Disposable Members

        protected override void DisposeNative()
        {
            del_aubio_source(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_source);
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public int Channels
        {
            get
            {
                ThrowIfDisposed();
                return aubio_source_get_channels(this).ToInt32();
            }
        }

        [PublicAPI]
        public Time Duration
        {
            get
            {
                ThrowIfDisposed();
                var samples = aubio_source_get_duration(this).ToInt32();
                var time = Time.FromSamples(SampleRate, samples);
                return time;
            }
        }

        [PublicAPI]
        public int SampleRate
        {
            get
            {
                ThrowIfDisposed();
                return aubio_source_get_samplerate(this).ToInt32();
            }
        }

        [PublicAPI]
        public void Close()
        {
            ThrowIfDisposed();
            ThrowIfNot(aubio_source_close(this));
        }

        [PublicAPI]
        public void Do([NotNull] FVec buffer, out int framesRead)
        {
            ThrowIfDisposed();

            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_source_do(this, buffer, out var read);

            framesRead = read.ToInt32();
        }

        [PublicAPI]
        public void Seek(int position)
        {
            ThrowIfDisposed();

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));

            ThrowIfNot(aubio_source_seek(this, position.ToUInt32()));
        }

        #endregion

        #region Native methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_source_close(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_source_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec readTo,
            out uint read
        );

        //TODO
        //void aubio_source_do_multi(aubio_source_t* s, fmat_t* read_to, uint_t* read)
        //[DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        // static extern void aubio_source_do_multi(
        //    [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance,
        //);

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_channels(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_duration(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_source_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_source_seek(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance,
            uint position
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))]
        private static extern unsafe Source__* new_aubio_source(
            [MarshalAs(UnmanagedType.LPStr)] string uri,
            uint sampleRate,
            uint hopSize
            );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_source(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(SourceMarshaler))] Source instance
        );

        #endregion
    }
}