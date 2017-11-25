using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    public sealed class LVec : AubioObject, IReadOnlyList<double>
    {
        public LVec(int length) : base(Create(length))
        {
            Length = length;
        }

        public LVec(IntPtr handle, bool isDisposable = true)
            : base(handle, isDisposable)
        {
        }

        [PublicAPI]
        public int Length { get; }

        private static IntPtr Create(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = Create(NativeMethods.new_lvec(length.ToUInt32()));

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_lvec(this);
        }

        [PublicAPI]
        public double[] GetData()
        {
            ThrowIfDisposed();

            var source = NativeMethods.lvec_get_data(this);
            var destination = new double[Length];
            Marshal.Copy(source, destination, 0, Length);

            return destination;
        }

        [PublicAPI]
        public void Ones()
        {
            ThrowIfDisposed();
            NativeMethods.lvec_ones(this);
        }

        [PublicAPI]
        public void Print()
        {
            ThrowIfDisposed();
            NativeMethods.lvec_print(this);
        }

        [PublicAPI]
        public void SetAll(float value)
        {
            ThrowIfDisposed();
            NativeMethods.lvec_set_all(this, value);
        }

        [PublicAPI]
        public void Zeros()
        {
            ThrowIfDisposed();
            NativeMethods.lvec_zeros(this);
        }

        #region IReadOnlyList<float> Members

        public double this[int index]
        {
            get
            {
                ThrowIfDisposed();
                var sample = NativeMethods.lvec_get_sample(this, index.ToUInt32());
                return sample;
            }
            set
            {
                ThrowIfDisposed();
                NativeMethods.lvec_set_sample(this, value, index.ToUInt32());
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            var destination = GetData();
            var enumerable = (IEnumerable<double>) destination;
            var enumerator = enumerable.GetEnumerator();

            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int IReadOnlyCollection<double>.Count => Length;

        #endregion
    }
}