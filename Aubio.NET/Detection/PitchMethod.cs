using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    [PublicAPI]
    public enum PitchMethod
    {
        Yin = 0,
        MultipleCombFilter = 1,
        SchimttTrigger = 2,
        FastHarmonicCombFilter = 3,
        YinFft = 4,
        YinFast = 5,
        SpectralAutoCorrelation = 6,
        Default = 7
    }
}