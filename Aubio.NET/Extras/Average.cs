using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aubio.NET.Extras
{
    [PublicAPI]
    public static class Average
    {
        public static IEnumerable<TResult> ZipForced<TFirst, TSecond, TResult>(
            [NotNull] this IEnumerable<TFirst> first,
            [NotNull] IEnumerable<TSecond> second,
            [NotNull] Func<TFirst, TSecond, TResult> selector)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext())
                {
                    if (!e2.MoveNext())
                        throw new ArgumentException($"{nameof(TSecond)} is shorter than {nameof(TFirst)}.");

                    yield return selector(e1.Current, e2.Current);
                }

                if (e2.MoveNext())
                    throw new ArgumentException($"{nameof(TFirst)} is shorter than {nameof(TSecond)}.");
            }
        }

        private static double NthRoot(double value, double n)
        {
            return Math.Pow(value, 1.0d / n);
        }

        private static double Sum(IEnumerable<double> values, out int len)
        {
            return Sum(values, out len, s => s);
        }

        private static double Sum(IEnumerable<double> values, out int len, [NotNull] Func<double, double> selector)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var sum = 0.0d;
            len = 0;

            foreach (var value in values)
            {
                sum += selector(value);
                len++;
            }

            if (len == 0)
                throw new ArgumentOutOfRangeException(nameof(values));

            return sum;
        }

        public static double Arithmetic([NotNull] IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var sum = Sum(values, out var len);
            var arithmetic = sum / len;
            return arithmetic;
        }

        public static double Cubic([NotNull] IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var sum = Sum(values, out var len, s => Math.Pow(s, 3.0d));
            var quadratic = NthRoot(sum / len, 3.0d);
            return quadratic;
        }

        public static double Geometric([NotNull] IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var sum = Sum(values, out var _);
            var geometric = NthRoot(sum, 3.0d);
            return geometric;
        }

        public static double Harmonic([NotNull] IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var sum = Sum(values, out var len, s => 1.0d / s);
            var harmonic = len / sum;
            return harmonic;
        }

        public static double Quadratic([NotNull] IEnumerable<double> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var sum = Sum(values, out var len, s => Math.Pow(s, 2.0d));
            var quadratic = NthRoot(sum / len, 2.0d);
            return quadratic;
        }

        public static double WeightedArithmetic(
            [NotNull] IEnumerable<double> values,
            [NotNull] IEnumerable<double> weights)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (weights == null)
                throw new ArgumentNullException(nameof(weights));

            var a = 0.0d;
            var b = 0.0d;
            var c = 0;

            var zip = ZipForced(values, weights, (value, weight) => new {value, weight});

            foreach (var pair in zip)
            {
                var v = pair.value;
                var w = pair.weight;

                a += w * v;
                b += w;
                c++;
            }

            if (c == 0)
                throw new ArgumentException();

            var weightedArithmetic = a / b;

            return weightedArithmetic;
        }

        public static double WeightedGeometric(
            [NotNull] IEnumerable<double> values,
            [NotNull] IEnumerable<double> weights)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (weights == null)
                throw new ArgumentNullException(nameof(weights));

            var a = 0.0d;
            var b = 0.0d;
            var c = 0;

            var zip = ZipForced(values, weights, (value, weight) => new {value, weight});

            foreach (var pair in zip)
            {
                var v = pair.value;
                var w = pair.weight;
                a += w * Math.Log(v);
                b += w;
                c++;
            }

            if (c == 0)
                throw new ArgumentException();

            var weightedGeometric = Math.Exp(a / b);

            return weightedGeometric;
        }

        public static double WeightedHarmonic(
            [NotNull] IEnumerable<double> values,
            [NotNull] IEnumerable<double> weights)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (weights == null)
                throw new ArgumentNullException(nameof(weights));

            var a = 0.0d;
            var b = 0.0d;
            var c = 0;

            var zip = ZipForced(values, weights, (value, weight) => new {value, weight});

            foreach (var pair in zip)
            {
                var v = pair.value;
                var w = pair.weight;

                a += w;
                b += w / v;
                c++;
            }

            if (c == 0)
                throw new ArgumentException();

            var weightedHarmonic = a / b;

            return weightedHarmonic;
        }
    }
}