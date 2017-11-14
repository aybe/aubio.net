using System.Linq;

namespace Aubio.TestApp.Desktop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // loads native part of aubio, you must do that before using it
            // and dispose it when you're done with it, here we just do 'using'

            using (new AubioNative(@"Aubio\x86\libaubio-6.dll", @"Aubio\x64\libaubio-6.dll"))
            {
                // aubio types are all IDisposable, make sure to dispose them when you're done
                // some of them won't get disposed like Filter.Feedback (LVec), this is done
                // purposefully because it would leave the Filter in an undefined state

                using (var vec = new FVec(1024))
                {
                    // FVec has an indexer
                    var length = vec.Length;
                    for (var i = 0; i < length; i++)
                        vec[i] = (float) i / length;

                    // the methods, including extensions
                    vec.Rev();
                    vec.Sin();
                    var dbSpl = vec.DbSpl();

                    // FVec is IEnumerable<float>, all vector types are
                    var array = vec.ToArray();
                }
            }
        }
    }
}