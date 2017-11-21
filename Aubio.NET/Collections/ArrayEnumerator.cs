using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aubio.NET.Collections
{
    internal sealed class ArrayEnumerator<T> : IEnumerator<T>
    {
        private readonly IArray<T> _array;
        private T _current;
        private int _index;

        public ArrayEnumerator([NotNull] IArray<T> array)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
            Reset();
        }

        void IDisposable.Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++_index >= _array.Length)
                return false;

            _current = _array[_index];
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public T Current => _current;

        object IEnumerator.Current => Current;
    }
}