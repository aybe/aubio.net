using System;
using System.IO;
using Aubio.NET.Collections;
using Aubio.NET.Detection;
using Aubio.NET.IO;

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

                    var noteVel = output[1];

                    var noteOff = output[2];
                    if (noteOff.AreNotEqual(0.0f))
                    {
                        var seconds = source.SamplesToSeconds(blocks * hopSize);
                        Console.WriteLine("OFF " + seconds.ToString("F6"));
                    }

                    var noteOn = output[0];
                    if (noteOn.AreNotEqual(0.0f))
                    {
                        var seconds = source.SamplesToSeconds(blocks * hopSize);
                        Console.Write($"ON {noteOn:F6}\t{seconds:F6}\t");
                    }


                    blocks++;
                } while (read == hopSize);
            }

            Console.ReadKey(true);
        }
    }
}