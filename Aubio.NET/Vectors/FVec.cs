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
        internal readonly unsafe FVec__* Handle;

        #endregion

        #region IVector<float> Members

        public unsafe float this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                return fvec_get_sample(Handle, (uint) index);
            }
            set
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                fvec_set_sample(Handle, value, (uint) index);
            }
        }

        public unsafe int Length => (int) Handle->Length;

        [PublicAPI]
        public unsafe void SetAll(float value)
        {
            fvec_set_all(Handle, value);
        }

        [PublicAPI]
        public unsafe void Ones()
        {
            fvec_ones(Handle);
        }

        [PublicAPI]
        public unsafe void Zeros()
        {
            fvec_zeros(Handle);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            return new VectorEnumerator<float>(this);
        }

        #endregion

        #region Public Members

        [PublicAPI]
        internal unsafe FVec(int length, bool isDisposable)
            : base(isDisposable)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = new_fvec((uint) length);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe FVec(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = new_fvec((uint) length);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe FVec(int length, FVecWindowType windowType)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var attribute = windowType.GetDescriptionAttribute();
            var description = attribute.Description;

            var handle = new_aubio_window(description, (uint) length);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe void Copy([NotNull] FVec target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.Length != Length)
                throw new ArgumentOutOfRangeException(nameof(target));

            fvec_copy(Handle, target.Handle);
        }

        [PublicAPI]
        public unsafe float* GetData()
        {
            return fvec_get_data(Handle);
        }

        [PublicAPI]
        public unsafe void Rev()
        {
            fvec_rev(Handle);
        }

        [PublicAPI]
        public unsafe void Print()
        {
            fvec_print(Handle);
        }

        [PublicAPI]
        public unsafe void Weight([NotNull] FVec weight)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            fvec_weight(Handle, weight.Handle);
        }

        [PublicAPI]
        public unsafe void WeightedCopy([NotNull] FVec weight, [NotNull] FVec output)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            fvec_weighted_copy(Handle, weight.Handle, output.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_fvec(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
        }

        #endregion

        #region Native methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FVec__* new_fvec(
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe FVec__* new_aubio_window(
            [MarshalAs(UnmanagedType.LPStr)] string windowType,
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_fvec(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_copy(
            FVec__* fVec,
            FVec__* target
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float* fvec_get_data(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float fvec_get_sample(
            FVec__* fVec,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_ones(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_print(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_rev(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_set_all(
            FVec__* fVec,
            float value
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_set_sample(
            FVec__* fVec,
            float value,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_weight(
            FVec__* fVec,
            FVec__* weight
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_weighted_copy(
            FVec__* fVec,
            FVec__* weight,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_zeros(
            FVec__* fVec
        );

        #endregion
    }
}