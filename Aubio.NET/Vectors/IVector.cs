using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    [PublicAPI]
    public interface IVector<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }
        int Length { get; }
    }
}