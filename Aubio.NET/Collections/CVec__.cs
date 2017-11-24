using System.Diagnostics.CodeAnalysis;

namespace Aubio.NET.Collections
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct CVec__
    {
#pragma warning disable 649
#pragma warning disable 169
        public readonly uint Length;
        public readonly unsafe float* Norm;
        public readonly unsafe float* Phas;
#pragma warning restore 169
#pragma warning restore 649
    }
}