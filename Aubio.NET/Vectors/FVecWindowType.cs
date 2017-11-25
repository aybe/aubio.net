using JetBrains.Annotations;

namespace Aubio.NET.Vectors
{
    [PublicAPI]
    public enum FVecWindowType
    {
        Ones = 0,
        Rectangle = 1,
        Hamming = 2,
        Hanning = 3,
        Hanningz = 4,
        Blackman = 5,
        BlackmanHarris = 6,
        Gaussian = 7,
        Welch = 8,
        Parzen = 9,
        Default = 10
    }
}