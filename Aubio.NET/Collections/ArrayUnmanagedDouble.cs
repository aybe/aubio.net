namespace Aubio.NET.Collections
{
    internal sealed unsafe class ArrayUnmanagedDouble : ArrayUnmanaged<double>
    {
        private readonly double* _data;

        public ArrayUnmanagedDouble(double* data, int length) : base(length)
        {
            _data = data;
        }

        public override double this[int index]
        {
            get
            {
                ThrowOnInvalidIndex(index);
                return _data[index];
            }

            set
            {
                ThrowOnInvalidIndex(index);
                _data[index] = value;
            }
        }
    }
}