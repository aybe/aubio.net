using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Temporal
{
    /// <summary>
    ///     https://aubio.org/doc/latest/resampler_8h.html
    /// </summary>
    public sealed class Resampler : AubioObject
    {
        // TODO ratio could be computed from two rates and then implement ISampler

        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Resampler__* Handle;

        #endregion

        #region Public Members

        public unsafe Resampler(float ratio, ResamplerType type)
        {
            if (ratio <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(ratio));

            Ratio = ratio;

            var handle = new_aubio_resampler(ratio, type);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public float Ratio { get; }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (output.Length < (int) Math.Ceiling(input.Length / Ratio))
                throw new ArgumentOutOfRangeException(nameof(output));

            aubio_resampler_do(Handle, input.Handle, output.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_resampler(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Resampler__* new_aubio_resampler(
            float ratio,
            ResamplerType type
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_resampler(
            Resampler__* resampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_resampler_do(
            Resampler__* resampler,
            FVec__* input,
            FVec__* output
        );

        #endregion
    }
}