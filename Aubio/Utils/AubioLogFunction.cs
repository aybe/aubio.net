using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.Utils
{
    [PublicAPI]
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
    public delegate void AubioLogFunction(AubioLogLevel level, string message, IntPtr data);
}