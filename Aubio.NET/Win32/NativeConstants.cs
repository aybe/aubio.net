using System.Diagnostics.CodeAnalysis;

namespace Aubio.NET.Win32
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal static class NativeConstants
    {
        public const int ERROR_SUCCESS = 0;
        public const int LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200;
        public const int LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;
        public const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;
        public const int LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400;
    }
}