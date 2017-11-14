using System;
using JetBrains.Annotations;

namespace Aubio.Extensions
{
    [PublicAPI]
    public static class NumericalExtensions
    {
        public static int ToInt32(this uint value)
        {
            return Convert.ToInt32(value);
        }

        public static uint ToUInt32(this int value)
        {
            return Convert.ToUInt32(value);
        }

        public static int ToInt32(this float value)
        {
            return Convert.ToInt32(value);
        }
    }
}