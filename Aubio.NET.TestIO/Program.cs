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

            //uri = @"C:\Users\Aybe\Music\1m.wav";

            if (!File.Exists(uri))
                throw new FileNotFoundException();

            var path = Path.ChangeExtension(uri, $".sink.{Path.GetExtension(uri)}");

            const int hopSize = 1024;

            using (var source = new Source(uri, 0, hopSize))
            using (var sink = new Sink(path))
            using (var buffer = new FMat(source.Channels, hopSize))
            {
                // TODO this sink does not resample !

                if (!sink.PresetSampleRate(44100))
                    throw new InvalidOperationException();

                if (!sink.PresetChannels(1))
                    throw new InvalidOperationException();

                var totalRead = 0;
                int framesRead;
                int blocks = 0;
                do
                {
                    source.DoMulti(buffer, out framesRead);

                    sink.DoMulti(buffer, framesRead);

                    totalRead += framesRead;
                    blocks++;

                    var percent = (float)totalRead / source.Duration.Samples;
                    PrintProgress(percent, 0, 0, 25);
                } while (framesRead == hopSize);
            }
        }

        private static void PrintProgress(float percent, int x, int y, int width)
        {
            if (percent < 0.0f || percent > 1.0f)
                throw new ArgumentOutOfRangeException(nameof(percent));

            if (x < 0)
                throw new ArgumentOutOfRangeException(nameof(x));

            if (y < 0)
                throw new ArgumentOutOfRangeException(nameof(y));

            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));

            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            var i = (int)(width * percent);
            var s1 = new string('█', i);
            var s2 = new string('▒', width - i);
            Console.Write($"{s1}{s2} {percent:P}");
        }
    }
}