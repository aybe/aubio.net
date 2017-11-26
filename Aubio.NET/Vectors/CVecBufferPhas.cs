using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    internal sealed class CVecBufferPhas : CVecBuffer
    {
        public unsafe CVecBufferPhas([NotNull] CVec cVec, [NotNull] float* data, int length)
            : base(cVec, data, length)
        {
        }

        public override float this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                return cvec_phas_get_sample(CVec, index.ToUInt32());
            }
            set
            {
                if (index < 0 || index >= Length)
                    throw new IndexOutOfRangeException();

                cvec_phas_set_sample(CVec, value, index.ToUInt32());
            }
        }

        public override unsafe float* GetData()
        {
            return cvec_phas_get_data(CVec);
        }

        public override void SetAll(float value)
        {
            cvec_phas_set_all(CVec, value);
        }

        public override void Ones()
        {
            cvec_phas_ones(CVec);
        }

        public override void Zeros()
        {
            cvec_phas_zeros(CVec);
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float* cvec_phas_get_data(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float cvec_phas_get_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_phas_ones(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_phas_set_all(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float value
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_phas_set_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float value,
            uint position
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_phas_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );
    }
}