using System;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Synthesis
{
    /// <summary>
    ///     https://aubio.org/doc/latest/wavetable_8h.html
    /// </summary>
    public sealed class Wavetable : AubioObject
    {
        #region Fields

        [NotNull]
        private readonly unsafe Wavetable__* _wavetable;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Wavetable(int sampleRate, int blockSize)
        {
            var wavetable = new_aubio_wavetable(sampleRate.ToUInt32(), blockSize.ToUInt32());
            if (wavetable == null)
                throw new ArgumentNullException(nameof(wavetable));

            _wavetable = wavetable;
        }

        [PublicAPI]
        public float Amplitude
        {
            get => aubio_wavetable_get_amp(this);
            set
            {
                if (aubio_wavetable_set_amp(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public float Frequency
        {
            get => aubio_wavetable_get_freq(this);
            set
            {
                if (aubio_wavetable_set_freq(this, value))
                    throw new InvalidOperationException();
            }
        }

        [PublicAPI]
        public bool IsPlaying => aubio_wavetable_get_playing(this);

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_wavetable_do(this, input, output);
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_wavetable_do_multi(this, input, output);
        }

        [PublicAPI]
        public bool Load([NotNull] string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return !aubio_wavetable_load(this, uri);
        }

        [PublicAPI]
        public bool Play()
        {
            return !aubio_wavetable_play(this);
        }

        [PublicAPI]
        public bool Stop()
        {
            return !aubio_wavetable_stop(this);
        }

        #endregion

        #region AubioObject Members

        protected override void DisposeNative()
        {
            del_aubio_wavetable(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_wavetable);
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
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_load(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable,
            [MarshalAs(UnmanagedType.LPStr)] string uri
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_wavetable_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_wavetable_do_multi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_wavetable(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_wavetable_get_amp(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_set_amp(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable,
            float amplitude
        );


        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_wavetable_get_freq(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_set_freq(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable,
            float frequency
        );


        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_get_playing(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );


        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_play(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );


        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_wavetable_stop(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Wavetable wavetable
        );

        #endregion
    }
}