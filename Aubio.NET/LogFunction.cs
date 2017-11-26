using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET
{
    [PublicAPI]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
    public delegate void LogFunction(LogLevel level, string message, IntPtr data);
}