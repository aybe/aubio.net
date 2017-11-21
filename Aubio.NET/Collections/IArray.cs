using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    [PublicAPI]
    public interface IArray<T> : IEnumerable<T>
    {
        T this[int index] { get; set; }
        int Length { get; }
    }
}