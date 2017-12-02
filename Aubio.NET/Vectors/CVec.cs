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
    ///     https://aubio.org/doc/latest/cvec_8h.html
    /// </summary>
    public sealed class CVec : AubioObject, IVector<CVecComplex>
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe CVec__* _cVec;

        #endregion

        #region Implementation of IVector<CVecComplex>

        public CVecComplex this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                var norm = Norm[index];
                var phas = Phas[index];
                var complex = new CVecComplex(norm, phas);
                return complex;
            }
            set
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                Norm[index] = value.Norm;
                Phas[index] = value.Phas;
            }
        }

        [PublicAPI]
        public unsafe int Length => _cVec->Length.ToInt32();

        public void SetAll(CVecComplex complex)
        {
            Norm.SetAll(complex.Norm);
            Phas.SetAll(complex.Phas);
        }

        public void Ones()
        {
            Norm.Ones();
            Phas.Ones();
        }

        [PublicAPI]
        public void Zeros()
        {
            cvec_zeros(this);
        }

        public IEnumerator<CVecComplex> GetEnumerator()
        {
            return new VectorEnumerator<CVecComplex>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public members

        [PublicAPI]
        public unsafe CVec(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var cVec = new_cvec(length.ToUInt32());
            if (cVec == null)
                throw new ArgumentNullException(nameof(cVec));

            _cVec = cVec;

            Norm = new CVecBufferNorm(this, cVec->Norm, cVec->Length.ToInt32()); // length is different here !
            Phas = new CVecBufferPhas(this, cVec->Phas, cVec->Length.ToInt32()); // length is different here !
        }

        [PublicAPI]
        public IVector<float> Norm { get; }

        [PublicAPI]
        public IVector<float> Phas { get; }

        [PublicAPI]
        public void Copy([NotNull] CVec target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.Length != Length)
                throw new ArgumentOutOfRangeException(nameof(target));

            cvec_copy(this, target);
        }

        [PublicAPI]
        public void LogMag(float lambda)
        {
            cvec_logmag(this, lambda);
        }

        [PublicAPI]
        public void Print()
        {
            cvec_print(this);
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_cvec(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_cVec);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe CVec__* new_cvec(
            uint length
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_cvec(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec target
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_logmag(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float lambda
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_print(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        #endregion
    }
}