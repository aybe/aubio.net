using System;
using System.IO;
using Aubio.NET.Collections;
using Aubio.NET.Detection;
using Aubio.NET.IO;

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
            using (var tempo = new Tempo("default", winSize, hopSize, source.SampleRate))
            {
                var frames = 0;
                int read;
                do
                {
                    source.Do(input, out read);
                    tempo.Do(input, output);

                    if (tempo.HasBeat(output))
                    {
                        var last = tempo.Last;
                        var bpm = tempo.Bpm;
                        var confidence = tempo.Confidence;
                        Console.WriteLine(
                            $"{nameof(last)}: {last}, " +
                            $"{nameof(bpm)}: {bpm:F3}, " +
                            $"{nameof(confidence)}: {confidence:F6}"
                        );
                    }

                    frames += read;
                } while (read == hopSize);
            }
            
            AubioUtils.Cleanup();
            Console.ReadLine();
        }
    }
}