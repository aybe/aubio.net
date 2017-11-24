using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aubio.NET.Collections
{
    public abstract class ArrayUnmanaged<T> : IArray<T>
    {
        private readonly int _length;

        protected ArrayUnmanaged(int length)
        {
            _length = length;
        }
        
        public abstract T this[int index] { get; set; }

        [SuppressMessage("ReSharper", "ConvertToAutoPropertyWhenPossible")]
        public int Length => _length;

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void ThrowOnInvalidIndex(int index)
        {
            if (index < 0 || index >= _length)
                throw new IndexOutOfRangeException();
        }
    }
}