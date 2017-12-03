using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Win32.Unused
{
    [UsedImplicitly]
    public sealed class NativeLibraryHandle : SafeHandle
    {
        private static readonly IntPtr InvalidHandleValue = IntPtr.Zero;

        public NativeLibraryHandle() : base(InvalidHandleValue, true)
        {
        }

        /// <inheritdoc />
        public override bool IsInvalid => handle == InvalidHandleValue;

        /// <inheritdoc />
        protected override bool ReleaseHandle()
        {
            return NativeMethods.FreeLibrary(handle);
        }
    }
}