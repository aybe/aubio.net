using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    /// <summary>
    ///     https://aubio.org/doc/latest/fvec_8h.html
    /// </summary>
    public sealed class FVec : AubioObject, IVector<float>
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe FVec__* _vec;

        #endregion

        #region Constructors

        [PublicAPI]
        internal unsafe FVec(int length, bool isDisposable)
            : base(isDisposable)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var fVec = new_fvec(length.ToUInt32());
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            _vec = fVec;
        }

        [PublicAPI]
        public unsafe FVec(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var fVec = new_fvec(length.ToUInt32());
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            _vec = fVec;
        }

        [PublicAPI]
        public unsafe FVec(int length, FVecWindowType windowType)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var fVec = new_aubio_window2(windowType, length.ToUInt32());
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            _vec = fVec;
        }

        #endregion

        #region IVector<float> Members

        public float this[int index]
        {
            get
            {
                ThrowOnInvalidIndex(index);
                return fvec_get_sample(this, index.ToUInt32());
            }
            set
            {
                ThrowOnInvalidIndex(index);
                fvec_set_sample(this, value, index.ToUInt32());
            }
        }

        public unsafe int Length => _vec->Length.ToInt32();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            return new VectorEnumerator<float>(this);
        }

        private void ThrowOnInvalidIndex(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException();
        }

        #endregion

        #region Disposable Members

        protected override void DisposeNative()
        {
            del_fvec(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_vec);
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public void Copy([NotNull] FVec target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            fvec_copy(this, target);
        }

        [PublicAPI]
        public unsafe float* GetData()
        {
            return fvec_get_data(this);
        }

        [PublicAPI]
        public void Rev()
        {
            fvec_rev(this);
        }

        [PublicAPI]
        public void SetAll(float value)
        {
            fvec_set_all(this, value);
        }

        [PublicAPI]
        public void Print()
        {
            fvec_print(this);
        }

        [PublicAPI]
        public void Ones()
        {
            fvec_ones(this);
        }

        [PublicAPI]
        public void Weight([NotNull] FVec weight)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            fvec_weight(this, weight);
        }

        [PublicAPI]
        public void WeightedCopy([NotNull] FVec weight, [NotNull] FVec output)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            fvec_weighted_copy(this, weight, output);
        }

        [PublicAPI]
        public void Zeros()
        {
            fvec_zeros(this);
        }

        #endregion

        #region Native methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec target
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float* fvec_get_data(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float fvec_get_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_ones(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_print(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_rev(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_set_all(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            float value
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_set_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            float value,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_weight(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec weight
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_weighted_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec weight,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_fvec(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FVec__* new_fvec(
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FVec__* new_aubio_window2(
            [MarshalAs(UnmanagedType.I4)] FVecWindowType windowType,
            uint length
        );

        #endregion
    }
}