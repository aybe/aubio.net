using System;
using System.IO;
using Aubio.NET.Collections;
using Aubio.NET.Detection;
using Aubio.NET.IO;

namespace Aubio.NET.TestPitch
{
    /// <summary>
    ///     https://aubio.org/doc/latest/pitch_2test-pitch_8c-example.html
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
            using (var pitch = new Pitch(PitchMethod.Yin, winSize, hopSize, source.SampleRate))
            {
                pitch.Silence += 0.1f;
                pitch.Tolerance += 0.1f;
                pitch.Unit = PitchUnit.Hertz;

                Console.WriteLine(
                    $"{nameof(pitch.Silence)} = {pitch.Silence}, " +
                    $"{nameof(pitch.Tolerance)} = {pitch.Tolerance}, " +
                    $"{nameof(pitch.Unit)} = {pitch.Unit}"
                );

                int read;
                var frames = 0;
                do
                {
                    source.Do(input, out read);
                    pitch.Do(input, output);
                    var seconds = source.SamplesToSeconds(frames);
                    var candidate = output[0];
                    var confidence = pitch.Confidence;
                    Console.WriteLine(
                        $"{nameof(seconds)} = {seconds:F6}, " +
                        $"{nameof(confidence)} = {confidence:F6}, " +
                        $"{nameof(candidate)} = {candidate:F6}"
                    );
                    frames += read;
                } while (read == hopSize);
            }

            AubioUtils.Cleanup();

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey(true);
        }
    }
}