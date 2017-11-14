using System;
using System.Runtime.InteropServices;

namespace Aubio.Interop.Win32
{
    public static class NativeMethods
    {
        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms684175(v=vs.85).aspx
        /// </summary>
        /// <param name="lpLibFileName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern NativeLibraryHandle LoadLibrary(string lpLibFileName);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh447159(v=vs.85).aspx
        /// </summary>
        /// <param name="lpwLibFileName"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadPackagedLibrary", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern NativeLibraryHandle LoadPackagedLibrary(string lpwLibFileName, uint reserved = 0);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152(v=vs.85).aspx
        /// </summary>
        /// <param name="hModule"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212(v=vs.85).aspx
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
    }
}