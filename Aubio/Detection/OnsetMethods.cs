using JetBrains.Annotations;

namespace Aubio.Detection
{
    [PublicAPI]
    public static class OnsetMethods
    {
        public const string Default = HighFrequencyContent;
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
}