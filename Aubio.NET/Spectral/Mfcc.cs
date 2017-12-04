using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/mfcc_8h.html
    /// </summary>
    public sealed class Mfcc : AubioObject, ISampler
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Mfcc__* Handle;

        #endregion

        #region Implementation of ISampler

        public int SampleRate { get; }

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe Mfcc(int bufferSize, int filters, int coefficients, int sampleRate = 44100)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (filters <= 0)
                throw new ArgumentOutOfRangeException(nameof(filters));

            if (coefficients <= 0)
                throw new ArgumentOutOfRangeException(nameof(coefficients));

            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            SampleRate = sampleRate;

            var handle = new_aubio_mfcc((uint) bufferSize, (uint) filters, (uint) coefficients, (uint) sampleRate);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe void Do([NotNull] CVec input, [NotNull] CVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_mfcc_do(Handle, input.Handle, output.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_mfcc(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Mfcc__* new_aubio_mfcc(
            uint bufferSize,
            uint filters,
            uint coefficients,
            uint sampleRate
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_mfcc(
            Mfcc__* mfcc
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_mfcc_do(
            Mfcc__* mfcc,
            CVec__* input,
            CVec__* output
        );

        #endregion
    }
}