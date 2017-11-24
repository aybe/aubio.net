using System;
using Aubio.NET.Vectors.Native;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    public abstract class CVecBuffer : VectorUnmanagedFloat
    {
        internal unsafe CVecBuffer([NotNull] CVec cVec, [NotNull] float* data, int length)
            : base(data, length)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            CVec = cVec ?? throw new ArgumentNullException(nameof(cVec));
        }

        protected CVec CVec { get; }

        public abstract unsafe float* GetData();

        public abstract void SetAll(float value);

        public abstract void Ones();

        public abstract void Zeros();
    }
}