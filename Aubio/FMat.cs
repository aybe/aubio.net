using System;
using System.Runtime.InteropServices;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio
{
    /// <summary>
    ///     https://aubio.org/doc/latest/fmat_8h.html
    /// </summary>
    public sealed class FMat : AubioObject
    {
        public FMat(int height, int length)
            : base(Create(height, length))
        {
            Height = height;
            Length = length;
        }

        public FMat(IntPtr handle, bool isDisposable = true)
            : base(handle, isDisposable)
        {
        }

        [PublicAPI]
        public float this[int row, int column]
        {
            get
            {
                ThrowIfDisposed();
                ThrowIfInvalid(row, column);
                var value = NativeMethods.fmat_get_sample(this, row.ToUInt32(), column.ToUInt32());
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfInvalid(row, column);
                NativeMethods.fmat_set_sample(this, value, row.ToUInt32(), column.ToUInt32());
            }
        }

        [PublicAPI]
        public int Height { get; }

        [PublicAPI]
        public int Length { get; }

        private void ThrowIfInvalid(int row, int column)
        {
            if (row < 0 || row >= Height)
                throw new ArgumentOutOfRangeException(nameof(row));

            if (column < 0 || column >= Length)
                throw new ArgumentOutOfRangeException(nameof(column));
        }

        private static IntPtr Create(int height, int length)
        {
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            var handle = NativeMethods.new_fmat(height.ToUInt32(), length.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_fmat(this);
        }

        [PublicAPI]
        public void Copy([NotNull] FMat target)
        {
            ThrowIfDisposed();

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (target.Height != Height || target.Length != Length)
                throw new ArgumentOutOfRangeException(nameof(target));

            NativeMethods.fmat_copy(this, target);
        }

        [PublicAPI]
        public FVec GetChannel(uint channel)
        {
            ThrowIfDisposed();

            using (var output = new FVec(Length))
            {
                NativeMethods.fmat_get_channel(this, channel, output);

                var target = new FVec(output.Length);

                output.Copy(target);

                return target;
            }
        }

        [PublicAPI]
        public float[] GetChannelData(uint channel)
        {
            ThrowIfDisposed();

            var source = NativeMethods.fmat_get_channel_data(this, channel);
            var destination = new float[Length];
            Marshal.Copy(source, destination, 0, Length);

            return destination;
        }

        [PublicAPI]
        public float[,] GetData()
        {
            // TODO https://eli.thegreenplace.net/2015/memory-layout-of-multi-dimensional-arrays/

            ThrowIfDisposed();

            var height = Height;
            var rows = Length;
            var source = NativeMethods.fmat_get_data(this);
            var destination = new float[rows];

            var result = new float[height, rows];

            for (var i = 0; i < height; i++)
            {
                Marshal.Copy(source, destination, 0, rows);

                for (var j = 0; j < rows; j++)
                    result[i, j] = destination[j];

                source += sizeof(float) * rows;
            }

            return result;
        }

        [PublicAPI]
        public float GetSample(int channel, uint position)
        {
            ThrowIfDisposed();
            var value = NativeMethods.fmat_get_sample(this, channel.ToUInt32(), position);
            return value;
        }

        [PublicAPI]
        public void Ones()
        {
            ThrowIfDisposed();
            NativeMethods.fmat_ones(this);
        }

        [PublicAPI]
        public void Print()
        {
            ThrowIfDisposed();
            NativeMethods.fmat_print(this);
        }

        [PublicAPI]
        public void Rev()
        {
            ThrowIfDisposed();
            NativeMethods.fmat_rev(this);
        }

        [PublicAPI]
        public void Set(float value)
        {
            ThrowIfDisposed();
            NativeMethods.fmat_set(this, value);
        }

        [PublicAPI]
        public void SetSample(float value, int channel, int position)
        {
            ThrowIfDisposed();
            NativeMethods.fmat_set_sample(this, value, channel.ToUInt32(), position.ToUInt32());
        }

        [PublicAPI]
        public FVec VecMul([NotNull] FVec scale)
        {
            ThrowIfDisposed();

            if (scale == null)
                throw new ArgumentNullException(nameof(scale));

            if (scale.Length != Length)
                throw new ArgumentOutOfRangeException(nameof(scale));

            var output = new FVec(Height);

            NativeMethods.fmat_vecmul(this, scale, output);

            return output;
        }

        [PublicAPI]
        public void Weight([NotNull] FMat weight)
        {
            ThrowIfDisposed();

            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            NativeMethods.fmat_weight(this, weight);
        }

        [PublicAPI]
        public void Zeros()
        {
            ThrowIfDisposed();
            NativeMethods.fmat_zeros(this);
        }
    }
}