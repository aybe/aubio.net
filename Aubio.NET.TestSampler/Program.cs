using System;
using Aubio.NET.IO;
using Aubio.NET.Synthesis;
using Aubio.NET.Test.Helper;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestSampler
{
    /// <summary>
    ///     https://aubio.org/doc/latest/synth_2test-sampler_8c-example.html
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var srcPath = ConsoleHelper.GetPath(args, 0);
            var smpPath = ConsoleHelper.GetPath(args, 1);
            var snkPath = ConsoleHelper.GetPath(args, 2, false);

            const int hopSize = 256;

            using (var vec = new FVec(hopSize))
            using (var source = new Source(srcPath, 0, hopSize))
            using (var sink = new Sink(snkPath, source.SampleRate))
            using (var sampler = new Sampler(source.SampleRate, hopSize))
            {
                if (!sampler.Load(smpPath))
                    throw new InvalidOperationException();

                int read;
                var nFrames = 0;

                do
                {
                    source.Do(vec, out read);

                    var i = nFrames / hopSize;
                    if (i == 10 || i == 40 || i == 70)
                        sampler.Play();

                    sampler.Do(vec, vec);
                    sink.Do(vec, read);

                    nFrames += read;
                } while (read == hopSize);
            }

            AubioUtils.Cleanup();
        }
    }
}