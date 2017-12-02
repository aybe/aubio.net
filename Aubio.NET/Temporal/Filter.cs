using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Temporal
{
    /// <summary>
    ///     https://aubio.org/doc/latest/filter_8h.html
    /// </summary>
    public sealed class Filter : AubioObject, ISampler
    {
        #region Fields

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Filter__* _filter;

        #endregion

        #region Implementation of ISampler

        public int SampleRate
        {
            get => aubio_filter_get_samplerate(this).ToInt32();

            [PublicAPI]
            set
            {
                if (aubio_filter_set_samplerate(this, value.ToUInt32()))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public int Order => aubio_filter_get_order(this).ToInt32();

        private unsafe Filter([NotNull] Filter__* filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            _filter = filter;
        }

        [PublicAPI]
        public void Do([NotNull] FVec input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            aubio_filter_do(this, input);
        }

        [PublicAPI]
        public void DoFiltFilt([NotNull] FVec input, [NotNull] FVec temp)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (temp == null)
                throw new ArgumentNullException(nameof(temp));

            aubio_filter_do_filtfilt(this, input, temp);
        }

        [PublicAPI]
        public void DoOutplace([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_filter_do_outplace(this, input, output);
        }

        [PublicAPI]
        public void DoReset()
        {
            aubio_filter_do_reset(this);
        }

        [PublicAPI]
        public unsafe LVec GetFeedback()
        {
            return new LVec(aubio_filter_get_feedback(this), false);
        }

        [PublicAPI]
        public unsafe LVec GetFeedforward()
        {
            return new LVec(aubio_filter_get_feedforward(this), false);
        }

        [PublicAPI]
        public static unsafe Filter New(int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            var filter = new Filter(new_aubio_filter(order.ToUInt32()));

            return filter;
        }

        [PublicAPI]
        public static unsafe Filter NewAWeighting(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var filter = new Filter(new_aubio_filter_a_weighting(sampleRate.ToUInt32()));

            return filter;
        }

        [PublicAPI]
        public static unsafe Filter NewCWeighting(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var filter = new Filter(new_aubio_filter_c_weighting(sampleRate.ToUInt32()));

            return filter;
        }

        [PublicAPI]
        public static unsafe Filter NewBiquad(double b0, double b1, double b2, double a1, double a2)
        {
            var filter = new Filter(new_aubio_filter_biquad(b0, b1, b2, a1, a2));

            return filter;
        }

        #endregion

        #region Overrides of AubioObject

        protected override void DisposeNative()
        {
            del_aubio_filter(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_filter);
        }

        #endregion

        #region Native Methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Filter__* new_aubio_filter(
            uint order
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Filter__* new_aubio_filter_a_weighting(
            uint sampleRate
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Filter__* new_aubio_filter_c_weighting(
            uint sampleRate
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Filter__* new_aubio_filter_biquad(
            double b0,
            double b1,
            double b2,
            double a1,
            double a2
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_filter(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_filter_do(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_filter_do_outplace(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec output
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_filter_do_filtfilt(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec input,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] FVec temp
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_filter_do_reset(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe LVec__* aubio_filter_get_feedback(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe LVec__* aubio_filter_get_feedforward(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_filter_get_order(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_filter_get_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_filter_set_samplerate(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))] Filter filter,
            uint sampleRate
        );

        // implemented factories instead for aubio_filter_set_* methods

        #endregion
    }
}