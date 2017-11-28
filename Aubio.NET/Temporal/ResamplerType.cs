using JetBrains.Annotations;

namespace Aubio.NET.Temporal
{
    [PublicAPI]
    public enum ResamplerType : uint
    {
        SincBestQuality = 0,
        SincMediumQuality = 1,
        SincFastest = 2,
        ZeroOrderHold = 3,
        Linear = 4
    }
}