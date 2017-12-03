using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;

namespace Aubio.NET.Win32.Unused
{
    internal static class NativeHelper
    {
        private static string GetDllDirectory()
        {
            var capacity = 2;

            while (true)
            {
                var builder = new StringBuilder(capacity);

                var directory = NativeMethods.GetDllDirectory((uint) capacity, builder);

                var exception = new Win32Exception();

                if (directory == 0 && exception.NativeErrorCode != NativeConstants.ERROR_SUCCESS)
                    throw exception;

                if (directory <= capacity)
                    return builder.ToString();

                capacity = (int) directory;
            }
        }

        public static string SetDllDirectory([CanBeNull] string lpPathName)
        {
            var directory = GetDllDirectory();

            if (!NativeMethods.SetDllDirectory(lpPathName))
                throw new Win32Exception();

            return directory;
        }
    }
}