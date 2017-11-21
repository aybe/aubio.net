// ReSharper disable once CheckNamespace

namespace System
{
    public static class Numerics
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

        public static bool AlmostEquals(this float a, float b)
        {
            // from Unity !
            var abs1 = Math.Abs(b - a);
            var max1 = Math.Max(Math.Abs(a), Math.Abs(b));
            var max2 = Math.Max(0.000001f * max1, float.Epsilon * 8.0f);
            var approximately = abs1 < max2;
            return approximately;
        }
    }
}