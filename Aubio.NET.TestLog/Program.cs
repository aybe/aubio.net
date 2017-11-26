using System;
using Aubio.NET.Detection;
using Aubio.NET.Vectors;

namespace Aubio.NET.TestLog
{
    internal static class Program
    {
        private static void Main()
        {
            // well, it is not thoroughly applied in sources, INF/DBG are absent
            // and Aubio.NET throws an exceptions if a call has failed

            // TODO over time see what's the best approach and fine tune this

            void PrintMessage(ConsoleColor color, string message)
            {
                var prev = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ForegroundColor = prev;
            }

            Log.SetLevelFunction(LogLevel.Debug,
                (level, message, data) => { PrintMessage(ConsoleColor.Magenta, message); },
                IntPtr.Zero);

            Log.SetLevelFunction(LogLevel.Error,
                (level, message, data) => { PrintMessage(ConsoleColor.Red, message); },
                IntPtr.Zero);

            Log.SetLevelFunction(LogLevel.Info,
                (level, message, data) => { PrintMessage(ConsoleColor.Green, message); },
                IntPtr.Zero);

            Log.SetLevelFunction(LogLevel.Message,
                (level, message, data) => { PrintMessage(ConsoleColor.Cyan, message); },
                IntPtr.Zero);

            Log.SetLevelFunction(LogLevel.Warning,
                (level, message, data) => { PrintMessage(ConsoleColor.Yellow, message); },
                IntPtr.Zero);

            using (var vec1 = new FVec(20))
            using (var vec2 = new FVec(10))
            {
                Console.WriteLine("Generate an error ...");
                vec1.Copy(vec2);

                Console.WriteLine("Generate a message ...");
                vec1.Print();
            }

            using (var pitch = new Pitch(PitchMethod.Default, 1024, 256, 44100))
            {
                try
                {
                    Console.WriteLine("Generate a warning ...");
                    pitch.Silence = 1;
                }
                catch (Exception e)
                {
                    PrintMessage(ConsoleColor.Red, "Aubio.NET however, will throw if state is invalid !");
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine("Press any key to exit . . .");
            Console.ReadKey(true);
        }
    }
}