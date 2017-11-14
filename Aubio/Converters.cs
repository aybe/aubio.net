using Aubio.Extensions;
using JetBrains.Annotations;

namespace Aubio
{
    [PublicAPI]
    public static class Converters
    {
        public static float SamplesToMilliseconds(int sampleRate, int samples)
        {
            return samples * 1000.0f / sampleRate;
        }

        public static float SamplesToSeconds(int sampleRate, int samples)
        {
            var milliseconds = SamplesToMilliseconds(sampleRate, samples);
            var seconds = MillisecondsToSeconds(milliseconds);
            return seconds;
        }

        public static int MillisecondsToSamples(int sampleRate, float milliseconds)
        {
            var samples = (milliseconds * sampleRate / 1000.0f).ToInt32();
            return samples;
        }

        public static float MillisecondsToSeconds(float milliseconds)
        {
            var seconds = milliseconds * 0.001f;
            return seconds;
        }

        public static float SecondsToMilliseconds(float seconds)
        {
            var milliseconds = seconds * 1000.0f;
            return milliseconds;
        }

        public static int SecondsToSamples(int sampleRate, float seconds)
        {
            var milliseconds = SecondsToMilliseconds(seconds);
            var samples = MillisecondsToSamples(sampleRate, milliseconds);
            return samples;
        }
    }
}