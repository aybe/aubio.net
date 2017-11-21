using System;
using Aubio.NET.Collections;

namespace Aubio.NET.Detection
{
    public static class TempoExtensions
    {
        public static bool HasBeat(this Tempo tempo, FVec output)
        {
            if (tempo == null)
                throw new ArgumentNullException(nameof(tempo));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            return !output[0].AlmostEquals(0.0f);
        }
    }
}