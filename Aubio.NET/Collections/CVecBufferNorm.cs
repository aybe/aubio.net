using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    internal sealed class CVecBufferNorm : CVecBuffer
    {
        public unsafe CVecBufferNorm([NotNull] CVec cVec, [NotNull] float* data, int length)
            : base(cVec, data, length)
        {
        }

        public override float this[int index]
        {
            get
            {
                ThrowOnInvalidIndex(index);
                return cvec_norm_get_sample(CVec, index.ToUInt32());
            }
            set
            {
                ThrowOnInvalidIndex(index);
                cvec_norm_set_sample(CVec, value, index.ToUInt32());
            }
        }

        public override void SetAll(float value)
        {
            cvec_norm_set_all(CVec, value);
        }

        public override void Ones()
        {
            cvec_norm_ones(CVec);
        }

        public override void Zeros()
        {
            cvec_norm_zeros(CVec);
        }

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float* cvec_norm_get_data(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float cvec_norm_get_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            uint position
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_norm_ones(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_norm_set_all(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_norm_set_sample(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec,
            float value,
            uint position
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void cvec_norm_zeros(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] CVec cVec
        );
    }
}