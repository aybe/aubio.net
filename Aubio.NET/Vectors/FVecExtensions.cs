using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    [PublicAPI]
    public static class FVecExtensions
    {
        [PublicAPI]
        public static void Exp([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_exp(fVec);
        }

        [PublicAPI]
        public static void Cos([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_cos(fVec);
        }

        [PublicAPI]
        public static void Sin([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_sin(fVec);
        }

        [PublicAPI]
        public static void Abs([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_abs(fVec);
        }

        [PublicAPI]
        public static void Sqrt([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_sqrt(fVec);
        }

        [PublicAPI]
        public static void Log10([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_log10(fVec);
        }

        [PublicAPI]
        public static void Log([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_log(fVec);
        }

        [PublicAPI]
        public static void Floor([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_floor(fVec);
        }

        [PublicAPI]
        public static void Ceil([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_ceil(fVec);
        }

        [PublicAPI]
        public static void Round([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_round(fVec);
        }

        [PublicAPI]
        public static void Pow([NotNull] this FVec fVec, float pow)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_pow(fVec, pow);
        }

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