using System.ComponentModel;
using JetBrains.Annotations;

namespace Aubio.NET.Detection
{
    [PublicAPI]
    public enum OnsetDetection
    {
        [Description("energy")]
        EnergyBased,

        [Description("hfc")]
        HighFrequencyContent,

        [Description("complex")]
        ComplexDomain,

        [Description("phase")]
        PhaseBased,

        [Description("wphase")]
        WeightedPhaseDeviation,

        [Description("specdiff")]
        SpectralDifference,

        [Description("specflux")]
        SpectralFlux,

        [Description("kl")]
        KullbackLiebler,

        [Description("mkl")]
        ModifiedKullbackLiebler
    }
}