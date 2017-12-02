using System;
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

        [NotNull]
        private readonly unsafe TransientSteadyStateSeparation__* _tss;

        private float _alpha = 3.0f;
        private float _beta = 4.0f;
        private float _threshold = 0.25f;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe TransientSteadyStateSeparation(int bufferSize, int hopSize)
        {
            var tss = new_aubio_tss(bufferSize.ToUInt32(), hopSize.ToUInt32());
            if (tss == null)
                throw new ArgumentNullException(nameof(tss));

            _tss = tss;
        }

        [PublicAPI]
        public float Alpha
        {
            get => _alpha;
            set
            {
                if (aubio_tss_set_alpha(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _alpha = value;
            }
        }

        [PublicAPI]
        public float Beta
        {
            get => _beta;
            set
            {
                if (aubio_tss_set_beta(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _beta = value;
            }
        }

        [PublicAPI]
        public float Threshold
        {
            get => _threshold;
            set
            {
                if (aubio_tss_set_threshold(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                _threshold = value;
            }
        }

        [PublicAPI]
        public void Do([NotNull] CVec input, [NotNull] CVec transient, [NotNull] CVec steady)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (transient == null)
                throw new ArgumentNullException(nameof(transient));

            if (steady == null)
                throw new ArgumentNullException(nameof(steady));

            aubio_tss_do(this, input, transient, steady);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_tss(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_tss);
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
        private static extern void del_aubio_tss(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            TransientSteadyStateSeparation separation
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_tss_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            TransientSteadyStateSeparation separation,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec transient,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec steady
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tss_set_threshold(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            TransientSteadyStateSeparation separation,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tss_set_alpha(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            TransientSteadyStateSeparation separation,
            float alpha
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_tss_set_beta(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            TransientSteadyStateSeparation separation,
            float beta
        );

        #endregion
    }
}