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

        internal unsafe FVec([NotNull] FVec__* vec, bool isDisposable)
            : base(isDisposable)
        {
            if (vec == null)
                throw new ArgumentNullException(nameof(vec));

            _vec = vec;
        }

        [PublicAPI]
        public unsafe FVec(int length, bool isDisposable = true)
            : this(new_fvec(length.ToUInt32()), isDisposable)
        {
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

        [PublicAPI]
        public void Exp()
        {
            fvec_exp(this);
        }

        [PublicAPI]
        public void Cos()
        {
            fvec_cos(this);
        }

        [PublicAPI]
        public void Sin()
        {
            fvec_sin(this);
        }

        [PublicAPI]
        public void Abs()
        {
            fvec_abs(this);
        }

        [PublicAPI]
        public void Sqrt()
        {
            fvec_sqrt(this);
        }

        [PublicAPI]
        public void Log10()
        {
            fvec_log10(this);
        }

        [PublicAPI]
        public void Log()
        {
            fvec_log(this);
        }

        [PublicAPI]
        public void Floor()
        {
            fvec_floor(this);
        }

        [PublicAPI]
        public void Ceil()
        {
            fvec_ceil(this);
        }

        [PublicAPI]
        public void Round()
        {
            fvec_round(this);
        }

        [PublicAPI]
        public void Pow(float pow)
        {
            fvec_pow(this, pow);
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
        private static extern void fvec_exp(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_cos(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_sin(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_abs(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_sqrt(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_log10(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_log(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_floor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_ceil(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_round(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_pow(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec instance,
            float pow
        );

        #endregion
    }
}