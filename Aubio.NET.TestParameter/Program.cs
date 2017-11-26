using System;
using Aubio.NET.Utilities;

namespace Aubio.NET.TestParameter
{
    internal static class Program
    {
        private static void Main()
        {
            using (var parameter = new Parameter(0, 100, 20))
            {
                var min = parameter.Min;
                var max = parameter.Max;
                Console.WriteLine($"{nameof(min)}: {min}, {nameof(max)}: {max}");

                var steps = parameter.Steps;

                for (var step = 0; step < steps; step++)
                {
                    var targetValue = parameter.SetTargetValue(20);

                    parameter.Min -= 1;
                    parameter.Max += 1;

                    var current = parameter.Current;
                    var next = parameter.GetNextValue();
                    Console.WriteLine(
                        $"{nameof(step)} {step} of {steps}, " +
                        $"{nameof(targetValue)}: {targetValue}, " +
                        $"{nameof(current)}: {current}, " +
                        $"{nameof(next)}: {next}"
                    );
                }
            }

            PressAnyKeyToExit();
        }

        private static void PressAnyKeyToExit()
        {
            Console.WriteLine("Press any key to exit . . .");
            Console.ReadKey(true);
        }
    }
}