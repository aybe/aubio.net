using System.Linq;
using Aubio.NET.Temporal;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestFilters
{
    internal static class Program
    {
        private static void Main()
        {
            using (var filter1 = Filter.NewAWeighting(44100))
            using (var filter2 = Filter.NewCWeighting(44100))
            using (var filter3 = Filter.NewBiquad(.1, .2, .3, .4, .5))
            using (var filter4 = new Filter(5))
            using (var input = new FVec(42, FVecWindowType.Ones))
            using (var output = new FVec(42))
            {
                Apply(filter1, input, output);
                Apply(filter2, input, output);
                Apply(filter3, input, output);
                Apply(filter4, input, output);
            }
        }

        private static void Apply(Filter filter, FVec input, FVec output)
        {
            output.Zeros();

            filter.SampleRate = 44100;

            filter.DoOutplace(input, output);

            var before = input.ToArray();
            var after = output.ToArray();

            using (var feedback = filter.GetFeedback())
            {

            }

            using (var feedforward = filter.GetFeedforward())
            {
            }
        }
    }
}