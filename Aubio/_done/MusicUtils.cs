using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Utils
{
    [PublicAPI]
    public static class MusicUtils
    {
        public static void Cleanup()
        {
            NativeMethods.aubio_cleanup();
        }

        public static float Unwrap2Pi(float phase)
        {
            var value = NativeMethods.aubio_unwrap2pi(phase);
            return value;
        }
    }
}