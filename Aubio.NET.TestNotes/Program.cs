using System;
using System.IO;
using Aubio.NET.Detection;
using Aubio.NET.IO;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestNotes
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var uri = args[0];

            if (!File.Exists(uri))
                throw new FileNotFoundException();

            const int winSize = 512;
            const int hopSize = 256;

            using (var source = new Source(uri, 0, hopSize))
            using (var input = new FVec(hopSize))
            using (var output = new FVec(hopSize))
            using (var notes = new Notes(winSize, hopSize, source.SampleRate))
            {
                int read;
                var blocks = 0;
                do
                {
                    source.Do(input, out read);
                    notes.Do(input, output);

                    var sec = source.SamplesToSeconds(blocks * hopSize);

                    var vel = output[1];

                    var off = output[2];
                    if (off.AreNotEqual(0.0f))
                        Console.WriteLine(
                            $"{nameof(off)} = {sec:F6}"
                        );

                    var on = output[0];
                    if (on.AreNotEqual(0.0f))
                        Console.Write(
                            $"{nameof(on)} = {on:F6}\t" +
                            $"{nameof(vel)} = {vel:F6}\t" +
                            $"{nameof(sec)} = {sec:F6}\t");


                    blocks++;
                } while (read == hopSize);
            }

            AubioUtils.Cleanup();

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey(true);
        }
    }
}