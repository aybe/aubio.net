using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/specdesc_8h.html
    /// </summary>
    public sealed class SpectralDescription : AubioObject
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe SpectralDescription__* Handle;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe SpectralDescription(SpectralDescriptor descriptor, int bufferSize = 1024)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var description = descriptor.GetAttribute<DescriptionAttribute>();
            if (description == null)
                throw new ArgumentNullException(nameof(description));

            var method = description.Description;
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            var handle = new_aubio_specdesc(method, (uint) bufferSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe void Do([NotNull] CVec fftGrain, [NotNull] FVec output)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_specdesc_do(Handle, fftGrain.Handle, output.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_specdesc(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe SpectralDescription__* new_aubio_specdesc(
            [MarshalAs(UnmanagedType.LPStr)] string method,
            uint bufferSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_specdesc(
            SpectralDescription__* description
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_specdesc_do(
            SpectralDescription__* description,
            CVec__* fftGrain,
            FVec__* desc
        );

        #endregion
    }
}