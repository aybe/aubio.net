using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tss_8h.html
    /// </summary>
    public sealed class TransientSteadyStateSeparation : AubioObject
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe TransientSteadyStateSeparation__* Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _alpha = 3.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _beta = 4.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _threshold = 0.25f;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe TransientSteadyStateSeparation(int bufferSize = 1024, int hopSize = 256)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = new_aubio_tss((uint) bufferSize, (uint) hopSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe float Alpha
        {
            get => _alpha;
            set
            {
                if (aubio_tss_set_alpha(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _alpha = value;
            }
        }

        [PublicAPI]
        public unsafe float Beta
        {
            get => _beta;
            set
            {
                if (aubio_tss_set_beta(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _beta = value;
            }
        }

        [PublicAPI]
        public unsafe float Threshold
        {
            get => _threshold;
            set
            {
                if (aubio_tss_set_threshold(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _threshold = value;
            }
        }

        [PublicAPI]
        public unsafe void Do([NotNull] CVec input, [NotNull] CVec transient, [NotNull] CVec steady)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (transient == null)
                throw new ArgumentNullException(nameof(transient));

            if (steady == null)
                throw new ArgumentNullException(nameof(steady));

            aubio_tss_do(Handle, input.Handle, transient.Handle, steady.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_tss(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe TransientSteadyStateSeparation__* new_aubio_tss(
            uint bufferSize,
            uint hopSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_tss(
            TransientSteadyStateSeparation__* separation
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_tss_do(
            TransientSteadyStateSeparation__* separation,
            CVec__* input,
            CVec__* transient,
            CVec__* steady
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tss_set_threshold(
            TransientSteadyStateSeparation__* separation,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tss_set_alpha(
            TransientSteadyStateSeparation__* separation,
            float alpha
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_tss_set_beta(
            TransientSteadyStateSeparation__* separation,
            float beta
        );

        #endregion
    }
}