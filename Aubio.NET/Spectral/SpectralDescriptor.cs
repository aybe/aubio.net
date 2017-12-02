using System.ComponentModel;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    [PublicAPI]
    public enum SpectralDescriptor
    {
        [Description("energy")]
        OnsetEnergyBased,

        [Description("hfc")]
        OnsetHighFrequencyContent,

        [Description("complex")]
        OnsetComplexDomain,

        [Description("phase")]
        OnsetPhaseBased,

        [Description("wphase")]
        OnsetWeightedPhaseDeviation,

        [Description("specdiff")]
        OnsetSpectralDifference,

        [Description("kl")]
        OnsetKullbackLiebler,

        [Description("mkl")]
        OnsetModifiedKullbackLiebler,

        [Description("specflux")]
        OnsetSpectralFlux,

        [Description("centroid")]
        SpectralCentroid,

        [Description("spread")]
        SpectralSpread,

        [Description("skewness")]
        SpectralSkewness,

        [Description("kurtosis")]
        SpectralKurtosis,

        [Description("slope")]
        SpectralSlope,

        [Description("decrease")]
        SpectralDecrease,

        [Description("rolloff")]
        SpectralRollOff,

        [Description("default")]
        SpectralDefault
    }
}