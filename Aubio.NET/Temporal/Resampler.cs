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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Resampler__* _resampler;

        #endregion

        #region Public Members

        public unsafe Resampler(float ratio, ResamplerType type)
        {
            if (ratio <= 0.0f)
                throw new ArgumentOutOfRangeException(nameof(ratio));

            Ratio = ratio;

            var resampler = new_aubio_resampler(ratio, type);
            if (resampler == null)
                throw new ArgumentNullException(nameof(resampler));

            _resampler = resampler;
        }

        [PublicAPI]
        public float Ratio { get; }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (output.Length < (int) Math.Ceiling(input.Length / Ratio))
                throw new ArgumentOutOfRangeException(nameof(output));

            aubio_resampler_do(this, input, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_resampler(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_resampler);
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
        private static extern void del_aubio_resampler(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Resampler resampler
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_resampler_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Resampler resampler,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        #endregion
    }
}