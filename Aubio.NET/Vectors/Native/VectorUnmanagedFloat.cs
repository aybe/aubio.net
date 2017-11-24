namespace Aubio.NET.Vectors.Native
{
    public unsafe class VectorUnmanagedFloat : VectorUnmanaged<float>
    {
        private readonly float* _data;

        public VectorUnmanagedFloat(float* data, int length)
            : base(length)
        {
            _data = data;
        }

        public override float this[int index]
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