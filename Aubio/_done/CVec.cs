using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    public sealed class CVec : AubioObject
    {
        public CVec(int length) : base(Create(length))
        {
            Length = length / 2 + 1;

            Norm = new Buffer(length,
                index =>
                {
                    ThrowIfDisposed();
                    var value = NativeMethods.cvec_norm_get_sample(this, index.ToUInt32());
                    return value;
                },
                (index, value) =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_norm_set_sample(this, value, index.ToUInt32());
                },
                value =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_norm_set_all(this, value);
                },
                () =>
                {
                    ThrowIfDisposed();
                    var source = NativeMethods.cvec_norm_get_data(this);
                    var destination = new float[Length];
                    Marshal.Copy(source, destination, 0, Length);
                    return destination;
                },
                () =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_norm_ones(this);
                },
                () =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_norm_zeros(this);
                });

            Phas = new Buffer(length,
                index =>
                {
                    ThrowIfDisposed();
                    var value = NativeMethods.cvec_phas_get_sample(this, index.ToUInt32());
                    return value;
                },
                (index, value) =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_phas_set_sample(this, value, index.ToUInt32());
                },
                value =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_phas_set_all(this, value);
                },
                () =>
                {
                    ThrowIfDisposed();
                    var source = NativeMethods.cvec_phas_get_data(this);
                    var destination = new float[Length];
                    Marshal.Copy(source, destination, 0, Length);
                    return destination;
                },
                () =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_phas_ones(this);
                },
                () =>
                {
                    ThrowIfDisposed();
                    NativeMethods.cvec_phas_zeros(this);
                });
        }

        private int Length { get; }

        [PublicAPI]
        public IBuffer Norm { get; }

        [PublicAPI]
        public IBuffer Phas { get; }

        private static IntPtr Create(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = Create(NativeMethods.new_cvec(length.ToUInt32()));

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_cvec(this);
        }

        [PublicAPI]
        public void Copy()
        {
            ThrowIfDisposed();
            NativeMethods.cvec_copy(this, this);
        }

        [PublicAPI]
        public void LogMag(float lambda)
        {
            ThrowIfDisposed();
            NativeMethods.cvec_logmag(this, lambda);
        }

        [PublicAPI]
        public void Print()
        {
            ThrowIfDisposed();
            NativeMethods.cvec_print(this);
        }

        [PublicAPI]
        public void Zeros()
        {
            ThrowIfDisposed();
            NativeMethods.cvec_zeros(this);
        }


        [PublicAPI]
        public interface IBuffer : IReadOnlyList<float>
        {
            void All(float value);
            void AllOnes();
            void AllZeros();
        }

        private sealed class Buffer : IBuffer
        {
            internal Buffer(
                int length,
                Func<int, float> cVecGet, Action<int, float> cVecSet, Action<float> cVecAll,
                Func<float[]> cVecData, Action cVecAllOnes, Action cVecAllZeros)
            {
                Length = length;
                CVecGet = cVecGet ?? throw new ArgumentNullException(nameof(cVecGet));
                CVecSet = cVecSet ?? throw new ArgumentNullException(nameof(cVecSet));
                CVecData = cVecData ?? throw new ArgumentNullException(nameof(cVecData));
                CVecAll = cVecAll ?? throw new ArgumentNullException(nameof(cVecAll));
                CVecAllOnes = cVecAllOnes ?? throw new ArgumentNullException(nameof(cVecAllOnes));
                CVecAllZeros = cVecAllZeros ?? throw new ArgumentNullException(nameof(cVecAllZeros));
            }

            private int Length { get; }
            private Func<int, float> CVecGet { get; }
            private Action<int, float> CVecSet { get; }
            private Func<float[]> CVecData { get; }
            private Action<float> CVecAll { get; }
            private Action CVecAllOnes { get; }
            private Action CVecAllZeros { get; }

            public void All(float value)
            {
                CVecAll(value);
            }

            public void AllOnes()
            {
                CVecAllOnes();
            }

            public void AllZeros()
            {
                CVecAllZeros();
            }

            #region   IReadOnlyList<float> Members

            public float this[int index]
            {
                get => CVecGet(index);
                set => CVecSet(index, value);
            }

            int IReadOnlyCollection<float>.Count => Length;

            public IEnumerator<float> GetEnumerator()
            {
                var data = CVecData();
                var enumerable = (IEnumerable<float>) data;
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
}