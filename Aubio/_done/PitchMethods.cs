using JetBrains.Annotations;

namespace Aubio.Detection
{
    [PublicAPI]
    public static class PitchMethods
    {
        public const string Default = YinFft;
        public const string SchimttTrigger = "schmitt";
        public const string FastHarmonicCombFilter = "fcomb";
        public const string MultipleCombFilter = "mcomb";
        public const string Yin = "yin";
        public const string YinFft = "yinfft";
    }
}