using System;
using System.IO;

namespace Aubio.NET.Test.Helper
{
    public static class ConsoleHelper
    {
        public static string GetPath(string[] args, int index, bool @throw =true)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            var fileName = args[index];

            if (@throw&& !File.Exists(fileName))
                throw new FileNotFoundException("File could not be found", fileName);

            return fileName;
        }

        public static string AppendSuffixToFileName(this string fileName, string suffix)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (suffix == null)
                throw new ArgumentNullException(nameof(suffix));

            var s1 = Path.GetFileNameWithoutExtension(fileName);
            var s2 = Path.GetExtension(fileName);
            var s3 = $"{s1}{suffix}{s2}";
            return s3;
        }
    }
}