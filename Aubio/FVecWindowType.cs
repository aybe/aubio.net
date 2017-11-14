using System.ComponentModel;
using JetBrains.Annotations;

namespace Aubio
{
    [PublicAPI]
    public enum FVecWindowType
    {
        [Description("default")] Default,

        [Description("rectangle")] Rectangle,

        [Description("hamming")] Hamming,

        [Description("hanning")] Hanning,

        [Description("hanningz")] HanningZ,

        [Description("blackman")] Blackman,

        [Description("blackman_harris")] BlackmanHarris,

        [Description("gaussian")] Gaussian,

        [Description("welch")] Welch,

        [Description("parzen")] Parzen
    }
}