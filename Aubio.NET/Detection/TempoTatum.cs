using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    [PublicAPI]
    public enum TempoTatum : uint
    {
        None = 0,
        Tatum = 1,
        Beat = 2
    }
}