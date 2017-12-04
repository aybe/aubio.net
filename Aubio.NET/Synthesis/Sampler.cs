using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Synthesis
{
    /// <summary>
    ///     https://aubio.org/doc/latest/sampler_8h.html
    /// </summary>
    public sealed class Sampler : AubioObject, ISampler
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Sampler__* Handle;

        #endregion

        #region Implementation of ISampler

        public int SampleRate { get; }

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Sampler(int sampleRate = 44100, int hopSize = 256)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));
         
            SampleRate = sampleRate;

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = new_aubio_sampler((uint) sampleRate, (uint) hopSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe bool IsPlaying => aubio_sampler_get_playing(Handle);

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_sampler_do(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_sampler_do_multi(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe bool Load([NotNull] string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return !aubio_sampler_load(Handle, uri);
        }

        [PublicAPI]
        public unsafe bool Play()
        {
            return !aubio_sampler_play(Handle);
        }

        [PublicAPI]
        public unsafe bool Stop()
        {
            return !aubio_sampler_stop(Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_sampler(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Sampler__* new_aubio_sampler(
            uint sampleRate,
            uint hopSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_sampler(
            Sampler__* sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_sampler_do(
            Sampler__* sampler,
            FVec__* input,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_sampler_do_multi(
            Sampler__* sampler,
            FMat__* input,
            FMat__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sampler_get_playing(
            Sampler__* sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sampler_load(
            Sampler__* sampler,
            [MarshalAs(UnmanagedType.LPStr)] string uri
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sampler_play(
            Sampler__* sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_sampler_stop(
            Sampler__* sampler
        );

        #endregion
    }
}