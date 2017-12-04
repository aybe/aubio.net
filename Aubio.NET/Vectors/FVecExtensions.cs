using System;
using System.Runtime.InteropServices;
using System.Security;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    [PublicAPI]
    public static class FVecExtensions
    {
        public static unsafe float DbSpl([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_db_spl(fVec.Handle);
        }

        public static unsafe float LevelDetection([NotNull] this FVec fVec, float threshold)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_level_detection(fVec.Handle, threshold);
        }

        public static unsafe float LevelLin([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_level_lin(fVec.Handle);
        }

        public static unsafe bool SilenceDetection([NotNull] this FVec fVec, float threshold)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_silence_detection(fVec.Handle, threshold);
        }

        public static unsafe float ZeroCrossingRate([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return aubio_zero_crossing_rate(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Abs([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_abs(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Ceil([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_ceil(fVec.Handle);
        }

        public static unsafe float Clamp([NotNull] this FVec fVec, float absmax)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            return fvec_clamp(fVec.Handle, absmax);
        }

        [PublicAPI]
        public static unsafe void Cos([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_cos(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Exp([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_exp(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Floor([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_floor(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Log([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_log(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Log10([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_log10(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Pow([NotNull] this FVec fVec, float pow)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_pow(fVec.Handle, pow);
        }

        [PublicAPI]
        public static unsafe void Round([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_round(fVec.Handle);
        }

        public static unsafe void SetWindowType([NotNull] this FVec fVec, FVecWindowType windowType)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            var attribute = windowType.GetDescriptionAttribute();
            var description = attribute.Description;

            if (fvec_set_window(fVec.Handle, description))
                throw new ArgumentOutOfRangeException(nameof(windowType));
        }

        [PublicAPI]
        public static unsafe void Sin([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_sin(fVec.Handle);
        }

        [PublicAPI]
        public static unsafe void Sqrt([NotNull] this FVec fVec)
        {
            if (fVec == null)
                throw new ArgumentNullException(nameof(fVec));

            fvec_sqrt(fVec.Handle);
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_db_spl(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_level_detection(
            FVec__* fVec,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_level_lin(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_silence_detection(
            FVec__* fVec,
            float threshold
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_zero_crossing_rate(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_abs(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_ceil(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float fvec_clamp(
            FVec__* fVec,
            float absmax
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_cos(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_exp(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_floor(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_log(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_log10(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_pow(
            FVec__* fVec,
            float pow
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_round(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool fvec_set_window(
            FVec__* fVec,
            [MarshalAs(UnmanagedType.LPStr)] string windowType
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_sin(
            FVec__* fVec
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void fvec_sqrt(
            FVec__* fVec
        );
    }
}