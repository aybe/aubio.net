using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    public sealed class CVec : AubioObject
    {
        #region Fields

        private readonly unsafe CVec__* _cVec;

        #endregion

        #region Constructors

        internal unsafe CVec([NotNull] CVec__* cVec)
        {
            if (cVec == null)
                throw new ArgumentNullException(nameof(cVec));

            _cVec = cVec;
            Norm = new CVecBufferNorm(this, cVec->Norm, cVec->Length.ToInt32());
            Phas = new CVecBufferNorm(this, cVec->Norm, cVec->Length.ToInt32());
        }

        [PublicAPI]
        public unsafe CVec(int length)
            : this(new_cvec(length.ToUInt32()))
        {
        }

        #endregion

        #region AubioObject members

        protected override void DisposeNative()
        {
            del_cvec(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_cVec);
        }

        #endregion

        #region Public members

        [PublicAPI]
        public unsafe int Length => _cVec->Length.ToInt32();

        [PublicAPI]
        public CVecBuffer Norm { get; }

        [PublicAPI]
        public CVecBuffer Phas { get; }

        [PublicAPI]
        public void Copy([NotNull] CVec target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

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

        [PublicAPI]
        public void Zeros()
        {
            cvec_zeros(this);
        }

        #endregion

        #region Native methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe CVec__* new_cvec(
            uint length
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_cvec(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_copy(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec target
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_logmag(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float lambda
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_print(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        #endregion
    }
}