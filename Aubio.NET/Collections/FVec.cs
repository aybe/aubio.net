using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    /// <summary>
    ///     https://aubio.org/doc/latest/fvec_8h.html
    /// </summary>
    public sealed class FVec : AubioObject, IArray<float>
    {
        #region Fields

        private readonly IArray<float> _data;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe FVec__* _vec;

        #endregion

        #region Constructors

        internal unsafe FVec(FVec__* vec)
        {
            if (vec == null)
                throw new ArgumentNullException(nameof(vec));

            _vec = vec;
            _data = new ArrayUnmanagedFloat(vec->Data, vec->Length.ToInt32());
        }

        [PublicAPI]
        public unsafe FVec(int length)
            : this(new_fvec(length.ToUInt32()))
        {
        }

        #endregion

        #region IArray<float> Members

        public float this[int index]
        {
            // bounds checked in implementation
            get => _data[index];
            set => _data[index] = value;
        }

        public int Length => _data.Length;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            return _data.GetEnumerator();
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
            ThrowIfDisposed();

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            fvec_copy(this, target);
        }

        [PublicAPI]
        public void Rev()
        {
            ThrowIfDisposed();
            fvec_rev(this);
        }

        [PublicAPI]
        public void SetAll(float value)
        {
            ThrowIfDisposed();
            fvec_set_all(this, value);
        }

        [PublicAPI]
        public void Ones()
        {
            ThrowIfDisposed();
            fvec_ones(this);
        }

        [PublicAPI]
        public void Weight([NotNull] FVec weight)
        {
            ThrowIfDisposed();

            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            fvec_weight(this, weight);
        }

        [PublicAPI]
        public void WeightedCopy([NotNull] FVec weight, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            fvec_weighted_copy(this, weight, output);
        }

        [PublicAPI]
        public void Zeros()
        {
            ThrowIfDisposed();
            fvec_zeros(this);
        }

        [PublicAPI]
        public void Exp()
        {
            ThrowIfDisposed();
            fvec_exp(this);
        }

        [PublicAPI]
        public void Cos()
        {
            ThrowIfDisposed();
            fvec_cos(this);
        }

        [PublicAPI]
        public void Sin()
        {
            ThrowIfDisposed();
            fvec_sin(this);
        }

        [PublicAPI]
        public void Abs()
        {
            ThrowIfDisposed();
            fvec_abs(this);
        }

        [PublicAPI]
        public void Sqrt()
        {
            ThrowIfDisposed();
            fvec_sqrt(this);
        }

        [PublicAPI]
        public void Log10()
        {
            ThrowIfDisposed();
            fvec_log10(this);
        }

        [PublicAPI]
        public void Log()
        {
            ThrowIfDisposed();
            fvec_log(this);
        }

        [PublicAPI]
        public void Floor()
        {
            ThrowIfDisposed();
            fvec_floor(this);
        }

        [PublicAPI]
        public void Ceil()
        {
            ThrowIfDisposed();
            fvec_ceil(this);
        }

        [PublicAPI]
        public void Round()
        {
            ThrowIfDisposed();
            fvec_round(this);
        }

        [PublicAPI]
        public void Pow(float pow)
        {
            ThrowIfDisposed();
            fvec_pow(this, pow);
        }

        #endregion

        #region Native methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec target
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float* fvec_get_data(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float fvec_get_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_ones(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_print(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_rev(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_set_all(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            float value
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_set_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            float value,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_weight(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec weight
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_weighted_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec weight,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_fvec(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FVec__* new_fvec(
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_exp(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_cos(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_sin(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_abs(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_sqrt(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_log10(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_log(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_floor(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_ceil(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_round(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void fvec_pow(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(FVecMarshaler))] FVec instance,
            float pow
        );

        #endregion
    }
}