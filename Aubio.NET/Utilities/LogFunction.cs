using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Utilities
{
    /// <summary>
    ///     https://aubio.org/doc/latest/log_8h.html
    /// </summary>
    [PublicAPI]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
    public delegate void LogFunction(LogLevel level, string message, IntPtr data);
}