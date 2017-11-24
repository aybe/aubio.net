namespace Aubio.NET.Vectors.Native
{
    public sealed unsafe class VectorUnmanagedDouble : VectorUnmanaged<double>
    {
        private readonly double* _data;

        public VectorUnmanagedDouble(double* data, int length)
            : base(length)
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