using System.Diagnostics.CodeAnalysis;

namespace Aubio.NET.Vectors
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal unsafe struct FVec__
    {
#pragma warning disable 649
        public readonly uint Length;
        public readonly float* Data;
#pragma warning restore 649
    }
}