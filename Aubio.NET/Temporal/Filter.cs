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

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Filter__* Handle;

        #endregion

        #region Implementation of ISampler

        public unsafe int SampleRate
        {
            get => (int) aubio_filter_get_samplerate(Handle);

            [PublicAPI]
            set
            {
                if (aubio_filter_set_samplerate(Handle, (uint) value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe int Order => (int) aubio_filter_get_order(Handle);

        private unsafe Filter([NotNull] Filter__* handle)
        {
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            aubio_filter_do(Handle, input.Handle);
        }

        [PublicAPI]
        public unsafe void DoFiltFilt([NotNull] FVec input, [NotNull] FVec temp)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (temp == null)
                throw new ArgumentNullException(nameof(temp));

            aubio_filter_do_filtfilt(Handle, input.Handle, temp.Handle);
        }

        [PublicAPI]
        public unsafe void DoOutplace([NotNull] FVec input, [NotNull] FVec output)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_filter_do_outplace(Handle, input.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void DoReset()
        {
            aubio_filter_do_reset(Handle);
        }

        [PublicAPI]
        public unsafe LVec GetFeedback()
        {
            return new LVec(aubio_filter_get_feedback(Handle), false);
        }

        [PublicAPI]
        public unsafe LVec GetFeedforward()
        {
            return new LVec(aubio_filter_get_feedforward(Handle), false);
        }

        [PublicAPI]
        public static unsafe Filter New(int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            var filter = new Filter(new_aubio_filter((uint) order));

            return filter;
        }

        [PublicAPI]
        public static unsafe Filter NewAWeighting(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var filter = new Filter(new_aubio_filter_a_weighting((uint) sampleRate));

            return filter;
        }

        [PublicAPI]
        public static unsafe Filter NewCWeighting(int sampleRate)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleRate));

            var filter = new Filter(new_aubio_filter_c_weighting((uint) sampleRate));

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

        protected override unsafe void DisposeNative()
        {
            del_aubio_filter(Handle);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(Handle);
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
        private static extern unsafe void del_aubio_filter(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_filter_do(
            Filter__* filter,
            FVec__* input
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_filter_do_outplace(
            Filter__* filter,
            FVec__* input,
            FVec__* output
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_filter_do_filtfilt(
            Filter__* filter,
            FVec__* input,
            FVec__* temp
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_filter_do_reset(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe LVec__* aubio_filter_get_feedback(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe LVec__* aubio_filter_get_feedforward(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_filter_get_order(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_filter_get_samplerate(
            Filter__* filter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_filter_set_samplerate(
            Filter__* filter,
            uint sampleRate
        );

        // implemented factories instead for aubio_filter_set_* methods

        #endregion
    }
}