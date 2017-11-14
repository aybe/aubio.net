using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Utils
{
    [PublicAPI]
    public static class Log
    {
        public static void Reset()
        {
            NativeMethods.aubio_log_reset();
        }

        public static void SetFunction([NotNull] AubioLogFunction function, IntPtr data)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            NativeMethods.aubio_log_set_function(function, data);
        }

        public static AubioLogFunction SetLevelFunction(AubioLogLevel level, [NotNull] AubioLogFunction function,
            IntPtr data)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            var value = NativeMethods.aubio_log_set_level_function(level, function, data);
            return value;
        }
    }
}