using JetBrains.Annotations;

namespace Aubio.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/specdesc_8h.html
    /// </summary>
    [PublicAPI]
    public static class SpectralDescriptors
    {
        [PublicAPI]
        public static class Onset
        {
            public const string EnergyBased = "energy";
            public const string HighFrequencyContent = "hfc";
            public const string ComplexDomain = "complex";
            public const string PhaseBased = "phase";
            public const string WeightedPhaseDeviation = "wphase";
            public const string SpectralDifference = "specdiff";
            public const string KullbackLiebler = "kl";
            public const string ModifiedKullbackLiebler = "mkl";
            public const string SpectralFlux = "specflux";
        }

        [PublicAPI]
        public static class Shape
        {
            public const string SpectralCentroid = "centroid";
            public const string SpectralSpread = "spread";
            public const string SpectralSkewness = "skewness";
            public const string SpectralKurtosis = "kurtosis";
            public const string SpectralSlope = "slope";
            public const string SpectralDecrease = "decrease";
            public const string SpectralRollOff = "rolloff";
        }
    }
}