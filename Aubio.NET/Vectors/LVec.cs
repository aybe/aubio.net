using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    public sealed class LVec : AubioObject, IVector<double>
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe LVec__* _lVec;

        #endregion

        #region Constructors

        private unsafe LVec([NotNull] LVec__* lVec)
        {
            if (lVec == null)
                throw new ArgumentNullException(nameof(lVec));

            _lVec = lVec;
        }

        [PublicAPI]
        public unsafe LVec(int length)
            : this(new_lvec(length.ToUInt32()))
        {
        }

        #endregion

        #region IVector<double> Members

        public double this[int index]
        {
            get => lvec_get_sample(this, index.ToUInt32());
            set => lvec_set_sample(this, value, index.ToUInt32());
        }

        public unsafe int Length => _lVec->Length.ToInt32();

        public IEnumerator<double> GetEnumerator()
        {
            return new VectorEnumerator<double>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [PublicAPI]
        public unsafe double* GetData()
        {
            return lvec_get_data(this);
        }

        public void SetAll(double value)
        {
            lvec_set_all(this, (float) value);
        }

        public void Ones()
        {
            lvec_ones(this);
        }

        public void Zeros()
        {
            lvec_zeros(this);
        }

        #endregion

        #region AubioObject Members

        protected override void DisposeNative()
        {
            del_lvec(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_lVec);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe LVec__* new_lvec(
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_lvec(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern double lvec_get_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lvec_set_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec,
            double value,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe double* lvec_get_data(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lvec_print(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lvec_set_all(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec,
            float value
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lvec_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lvec_ones(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] LVec lVec
        );

        #endregion
    }
}