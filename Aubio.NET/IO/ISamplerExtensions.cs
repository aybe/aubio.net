using JetBrains.Annotations;

namespace Aubio.NET.IO
{
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public static class ISamplerExtensions
    {
        public static int MillisecondsToSamples(this ISampler sampler, float milliseconds)
        {
            return Converters.MillisecondsToSamples(sampler.SampleRate, milliseconds);
        }

        public static float MillisecondsToSeconds(this ISampler sampler, float milliseconds)
        {
            return Converters.MillisecondsToSeconds(milliseconds);
        }

        public static float SamplesToMilliseconds(this ISampler sampler, int samples)
        {
            return Converters.SamplesToMilliseconds(sampler.SampleRate, samples);
        }

        public static float SamplesToSeconds(this ISampler sampler, int samples)
        {
            return Converters.SamplesToSeconds(sampler.SampleRate, samples);
        }

        public static float SecondsToMilliseconds(this ISampler sampler, float seconds)
        {
            return Converters.SecondsToMilliseconds(seconds);
        }

        public static int SecondsToSamples(this ISampler sampler, float seconds)
        {
            return Converters.SecondsToSamples(sampler.SampleRate, seconds);
        }

        public static Time TimeFromMilliseconds(this ISampler sampler, float milliseconds)
        {
            return Time.FromMilliseconds(sampler.SampleRate, milliseconds);
        }

        public static Time TimeFromSamples(this ISampler sampler, int samples)
        {
            return Time.FromSamples(sampler.SampleRate, samples);
        }

        public static Time TimeFromSeconds(this ISampler sampler, float seconds)
        {
            return Time.FromSeconds(sampler.SampleRate, seconds);
        }
    }
}