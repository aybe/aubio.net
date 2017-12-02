using System;
using System.IO;
using Aubio.NET.Detection;
using Aubio.NET.IO;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestTempo
{
    /// <summary>
    ///     https://aubio.org/doc/latest/tempo_2test-tempo_8c-example.html
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var uri = args[0];

            if (!File.Exists(uri))
                throw new FileNotFoundException();

            const int winSize = 1024;
            const int hopSize = winSize / 4;

            using (var source = new Source(uri, 0, hopSize))
            using (var input = new FVec(hopSize))
            using (var output = new FVec(1))
            using (var tempo = new Tempo(winSize, hopSize, source.SampleRate))
            {
                var frames = 0;
                int read;
                do
                {
                    source.Do(input, out read);
                    tempo.Do(input, output);

                    if (tempo.HasBeat(output))
                    {
                        var seconds = source.SamplesToSeconds(frames);
                        var beat = tempo.Last;
                        var bpm = tempo.Bpm;
                        var confidence = tempo.Confidence;
                        var period = tempo.Period.Seconds;
                        Console.WriteLine(
                            $"{nameof(seconds)}: {seconds:F3}, " +
                            $"{nameof(beat)}: {beat.Seconds:F3}, " +
                            $"{nameof(bpm)}: {bpm:F3}, " +
                            $"{nameof(confidence)}: {confidence:F6}, " +
                            $"{nameof(period)}: {period:F3}"
                        );
                    }

                    frames += read;
                } while (read == hopSize);
            }

            AubioUtils.Cleanup();

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey(true);
        }
    }
}