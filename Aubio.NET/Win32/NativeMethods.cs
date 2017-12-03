using System;
using System.Runtime.InteropServices;
using System.Text;
using Aubio.NET.Win32.Unused;

namespace Aubio.NET.Win32
{
    internal static class NativeMethods
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

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms683186(v=vs.85).aspx
        /// </summary>
        /// <param name="nBufferLength"></param>
        /// <param name="lpBuffer"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetDllDirectory(uint nBufferLength, [Out] StringBuilder lpBuffer);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms686203(v=vs.85).aspx
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetDllDirectory(string lpPathName);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh310513(v=vs.85).aspx
        /// </summary>
        /// <param name="newDirectory"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr AddDllDirectory(string newDirectory);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh310514(v=vs.85).aspx
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveDllDirectory(IntPtr cookie);

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh310515(v=vs.85).aspx
        /// </summary>
        /// <param name="directoryFlags"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetDefaultDllDirectories(uint directoryFlags);
    }
}