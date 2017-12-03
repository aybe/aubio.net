using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Win32.Unused
{
    /// <summary>
    ///     Helper for loading a native library.
    /// </summary>
    [PublicAPI]
    public class NativeLibraryLoader : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeLibraryLoader" /> class.
        /// </summary>
        /// <param name="path32">
        ///     Path to the 32-bit binary of the library.
        /// </param>
        /// <param name="path64">
        ///     Path to the 64-bit binary of the library.
        /// </param>
        /// <exception cref="FileNotFoundException">
        ///     <paramref name="path64" /> or <paramref name="path64" /> were not found.
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        ///     The library could not be loaded.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     The platform is not supported.
        /// </exception>
        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        public NativeLibraryLoader(string path32, string path64)
        {
            if (!File.Exists(path32))
                throw new FileNotFoundException("32-bit library not found.", path32);

            if (!File.Exists(path64))
                throw new FileNotFoundException("64-bit library not found.", path64);

            string path;

            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    path = path32;
                    break;
                case Architecture.X64:
                    path = path64;
                    break;
                default:
                    throw new PlatformNotSupportedException();
            }

            NativeLibraryHandle handle;

            var description = RuntimeInformation.FrameworkDescription;
            if (description.StartsWith(".NET Framework"))
                handle = NativeMethods.LoadLibrary(path);
            else if (description.StartsWith(".NET Core"))
                handle = NativeMethods.LoadPackagedLibrary(path);
            else
                throw new PlatformNotSupportedException();

            if (handle.IsInvalid)
                throw new InvalidOperationException("Library could not be loaded.", new Win32Exception());

            Handle = handle;
        }

        [NotNull]
        private NativeLibraryHandle Handle { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Handle.IsInvalid == false)
                Handle.Dispose();
        }

        /// <inheritdoc />
        ~NativeLibraryLoader()
        {
            Dispose(false);
        }
    }
}