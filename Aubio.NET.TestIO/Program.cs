using System;
using System.IO;
using Aubio.NET.IO;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestIO
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var uri = args[0];

            if (!File.Exists(uri))
                throw new FileNotFoundException();

            const int hopSize = 1024;

            using (var source = new Source(uri, 0, hopSize))
            using (var sink = new Sink("test.wav"))
            using (var buffer = new FMat(source.Channels, hopSize))
            {
                // TODO this sink does not resample !

                if (!sink.PresetSampleRate(44100))
                    throw new InvalidOperationException();

                if (!sink.PresetChannels(1))
                    throw new InvalidOperationException();

                var totalRead = 0;
                int framesRead;

                do
                {
                    source.DoMulti(buffer, out framesRead);
                    sink.DoMulti(buffer, framesRead);

                    totalRead += framesRead;

                    Console.WriteLine($"{(float) totalRead / source.Duration.Samples:P}");
                } while (framesRead == hopSize);
            }
        }
    }
}