namespace Aubio.NET.Collections
{
    public unsafe class ArrayUnmanagedFloat : ArrayUnmanaged<float>
    {
        private readonly float* _data;

        public ArrayUnmanagedFloat(float* data, int length) 
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