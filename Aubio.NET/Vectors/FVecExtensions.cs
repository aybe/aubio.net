using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    [PublicAPI]
    public static class FVecExtensions
    {
        public static float ZeroCrossingRate([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_zero_crossing_rate(fVec);
        }

        public static float LevelLin([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_level_lin(fVec);
        }

        public static float DbSpl([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_db_spl(fVec);
        }

        public static bool SilenceDetection([NotNull] this FVec fVec, float threshold)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_silence_detection(fVec, threshold);
        }

        public static float LevelDetection([NotNull] this FVec fVec, float threshold)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_level_detection(fVec, threshold);
        }

        public static float Clamp([NotNull] this FVec fVec, float absmax)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return fvec_clamp(fVec, absmax);
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        private static extern float aubio_zero_crossing_rate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        private static extern float aubio_level_lin(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        private static extern float aubio_db_spl(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_silence_detection(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        private static extern float aubio_level_detection(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio")]
        private static extern float fvec_clamp(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec vec,
            float absmax
        );
    }
}