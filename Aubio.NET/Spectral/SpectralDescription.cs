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

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe SpectralDescription__* _specdesc;

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

            var specdesc = new_aubio_specdesc(method, bufferSize.ToUInt32());
            if (specdesc == null)
                throw new ArgumentNullException(nameof(specdesc));

            _specdesc = specdesc;
        }

        [PublicAPI]
        public void Do([NotNull] CVec fftGrain, [NotNull] FVec output)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_specdesc_do(this, fftGrain, output);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_specdesc(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_specdesc);
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
        private static extern void del_aubio_specdesc(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralDescription description
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_specdesc_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            SpectralDescription description,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec fftGrain,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec desc
        );

        #endregion
    }
}