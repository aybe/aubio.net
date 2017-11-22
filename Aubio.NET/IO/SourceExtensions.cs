using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    [PublicAPI]
    public static class SourceExtensions
    {
        public static int MillisecondsToSamples(this Source source, float milliseconds)
        {
            return Converters.MillisecondsToSamples(source.SampleRate, milliseconds);
        }

        public static float MillisecondsToSeconds(this Source source, float milliseconds)
        {
            return Converters.MillisecondsToSeconds(milliseconds);
        }

        public static float SamplesToMilliseconds(this Source source, int samples)
        {
            return Converters.SamplesToMilliseconds(source.SampleRate, samples);
        }

        public static float SamplesToSeconds(this Source source, int samples)
        {
            return Converters.SamplesToSeconds(source.SampleRate, samples);
        }

        public static float SecondsToMilliseconds(this Source source, float seconds)
        {
            return Converters.SecondsToMilliseconds(seconds);
        }

        public static int SecondsToSamples(this Source source, float seconds)
        {
            return Converters.SecondsToSamples(source.SampleRate, seconds);
        }

        public static Time TimeFromMilliseconds(this Source source, float milliseconds)
        {
            return Time.FromMilliseconds(source.SampleRate, milliseconds);
        }

        public static Time TimeFromSamples(this Source source, int samples)
        {
            return Time.FromSamples(source.SampleRate, samples);
        }

        public static Time TimeFromSeconds(this Source source, float seconds)
        {
            return Time.FromSeconds(source.SampleRate, seconds);
        }
    }
}