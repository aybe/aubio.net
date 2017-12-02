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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Sampler__* _sampler;

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

            var sampler = new_aubio_sampler(sampleRate.ToUInt32(), hopSize.ToUInt32());
            if (sampler == null)
                throw new ArgumentNullException(nameof(sampler));

            _sampler = sampler;
        }

        [PublicAPI]
        public bool IsPlaying => aubio_sampler_get_playing(this);

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_sampler_do(this, input, output);
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_sampler_do_multi(this, input, output);
        }

        [PublicAPI]
        public bool Load([NotNull] string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return !aubio_sampler_load(this, uri);
        }

        [PublicAPI]
        public bool Play()
        {
            return !aubio_sampler_play(this);
        }

        [PublicAPI]
        public bool Stop()
        {
            return !aubio_sampler_stop(this);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_sampler(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_sampler);
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
        private static extern void del_aubio_sampler(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_sampler_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_sampler_do_multi(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FMat output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sampler_get_playing(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sampler_load(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler,
            [MarshalAs(UnmanagedType.LPStr)] string uri
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sampler_play(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_sampler_stop(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Sampler sampler
        );

        #endregion
    }
}