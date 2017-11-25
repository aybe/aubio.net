using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    public class FVec : AubioObject, IReadOnlyList<float>
    {
        protected FVec(IntPtr handle) : base(handle)
        {
        }

        public FVec(int length) : base(Create(length))
        {
            Length = length;
        }

        [PublicAPI]
        public int Length { get; }

        private static IntPtr Create(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = Create(NativeMethods.new_fvec(length.ToUInt32()));

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_fvec(this);
        }

        [PublicAPI]
        public void Copy([NotNull] FVec target)
        {
            ThrowIfDisposed();

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.Length != Length)
                throw new ArgumentOutOfRangeException(nameof(target));

            NativeMethods.fvec_copy(this, target);
        }

        [PublicAPI]
        public float[] GetData()
        {
            ThrowIfDisposed();

            var source = NativeMethods.fvec_get_data(this);
            var destination = new float[Length];
            Marshal.Copy(source, destination, 0, Length);

            return destination;
        }

        [PublicAPI]
        public void Ones()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_ones(this);
        }

        [PublicAPI]
        public void Print()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_print(this);
        }

        [PublicAPI]
        public void Rev()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_rev(this);
        }

        [PublicAPI]
        public void SetAll(float value)
        {
            ThrowIfDisposed();
            NativeMethods.fvec_set_all(this, value);
        }

        [PublicAPI]
        public void WeightedCopy([NotNull] FVec weight, [NotNull] FVec target)
        {
            ThrowIfDisposed();

            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            NativeMethods.fvec_weighted_copy(this, weight, target);
        }

        [PublicAPI]
        public void Zeros()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_zeros(this);
        }

        #region Extensions

        [PublicAPI]
        public void Exp()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_exp(this);
        }

        [PublicAPI]
        public void Cos()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_cos(this);
        }

        [PublicAPI]
        public void Sin()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_sin(this);
        }

        [PublicAPI]
        public void Abs()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_abs(this);
        }

        [PublicAPI]
        public void Sqrt()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_sqrt(this);
        }

        [PublicAPI]
        public void Log10()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_log10(this);
        }

        [PublicAPI]
        public void Log()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_log(this);
        }

        [PublicAPI]
        public void Floor()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_floor(this);
        }

        [PublicAPI]
        public void Ceil()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_ceil(this);
        }

        [PublicAPI]
        public void Round()
        {
            ThrowIfDisposed();
            NativeMethods.fvec_round(this);
        }

        [PublicAPI]
        public void Pow(float pow)
        {
            ThrowIfDisposed();
            NativeMethods.fvec_pow(this, pow);
        }

        #endregion

        #region Music

        [PublicAPI]
        public void Clamp(float absmax)
        {
            ThrowIfDisposed();
            NativeMethods.fvec_clamp(this, absmax);
        }

        [PublicAPI]
        public float DbSpl()
        {
            var value = NativeMethods.aubio_db_spl(this);
            return value;
        }

        [PublicAPI]
        public float LevelDetection(float threshold)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_level_detection(this, threshold);
            return value;
        }

        [PublicAPI]
        public float LevelLin(float threshold)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_level_lin(this);
            return value;
        }

        [PublicAPI]
        public bool SilenceDetection(float threshold)
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_silence_detection(this, threshold);
            return value;
        }

        [PublicAPI]
        public float ZeroCrossingRate()
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_zero_crossing_rate(this);
            return value;
        }

        #endregion

        #region IReadOnlyList<float> Members

        [PublicAPI]
        public float this[int index]
        {
            get
            {
                ThrowIfDisposed();
                var sample = NativeMethods.fvec_get_sample(this, index.ToUInt32());
                return sample;
            }
            set
            {
                ThrowIfDisposed();
                NativeMethods.fvec_set_sample(this, value, index.ToUInt32());
            }
        }

        int IReadOnlyCollection<float>.Count => Length;

        public IEnumerator<float> GetEnumerator()
        {
            var destination = GetData();
            var enumerable = (IEnumerable<float>) destination;
            var enumerator = enumerable.GetEnumerator();

            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}