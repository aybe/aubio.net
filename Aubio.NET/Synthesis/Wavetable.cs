using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Synthesis
{
    /// <summary>
    ///     https://aubio.org/doc/latest/wavetable_8h.html
    /// </summary>
    public sealed class Wavetable : AubioObject, ISampler
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Wavetable__* Handle;

        #endregion

        #region Implementation of ISampler

        public int SampleRate { get; }

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Wavetable(int sampleRate = 44100, int blockSize = 1024)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            SampleRate = sampleRate;

            if (blockSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockSize));

            var handle = new_aubio_wavetable((uint) sampleRate, (uint) blockSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe float Amplitude
        {
            get => aubio_wavetable_get_amp(Handle);
            set
            {
                if (aubio_wavetable_set_amp(Handle, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe float Frequency
        {
            get => aubio_wavetable_get_freq(Handle);
            set
            {
                if (aubio_wavetable_set_freq(Handle, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public unsafe bool IsPlaying => aubio_wavetable_get_playing(Handle);

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_wavetable_do(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_wavetable_do_multi(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe bool Load([NotNull] string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return !aubio_wavetable_load(Handle, uri);
        }

        [PublicAPI]
        public unsafe bool Play()
        {
            return !aubio_wavetable_play(Handle);
        }

        [PublicAPI]
        public unsafe bool Stop()
        {
            return !aubio_wavetable_stop(Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_wavetable(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Wavetable__* new_aubio_wavetable(
            uint sampleRate,
            uint blockSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_wavetable(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_load(
            Wavetable__* wavetable,
            [MarshalAs(UnmanagedType.LPStr)] string uri
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_play(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_stop(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_wavetable_do(
            Wavetable__* wavetable,
            FVec__* input,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_wavetable_do_multi(
            Wavetable__* wavetable,
            FMat__* input,
            FMat__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_wavetable_get_amp(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_wavetable_get_freq(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_get_playing(
            Wavetable__* wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_set_amp(
            Wavetable__* wavetable,
            float amplitude
        );


        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_wavetable_set_freq(
            Wavetable__* wavetable,
            float frequency
        );

        #endregion
    }
}