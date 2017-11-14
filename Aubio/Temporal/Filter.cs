using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Temporal
{
    public class Filter : AubioObject
    {
        protected Filter(IntPtr handle)
            : base(handle)
        {
        }

        [PublicAPI]
        public Filter(int order)
            : base(Create(order))
        {
        }

        [PublicAPI]
        public LVec Feedback
        {
            get
            {
                ThrowIfDisposed();

                var handle = NativeMethods.aubio_filter_get_feedback(this);
                var value = new LVec(handle, false);
                return value;
            }
        }

        [PublicAPI]
        public LVec Feedforward
        {
            get
            {
                ThrowIfDisposed();

                var handle = NativeMethods.aubio_filter_get_feedforward(this);
                var value = new LVec(handle, false);
                return value;
            }
        }

        public int Order
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_filter_get_order(this);
                return value.ToInt32();
            }
        }

        [PublicAPI]
        public int SampleRate
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_filter_get_samplerate(this);
                return value.ToInt32();
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_filter_set_samplerate(this, value.ToUInt32()));
            }
        }

        private static IntPtr Create(int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            var handle = NativeMethods.new_aubio_filter(order.ToUInt32());
            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_filter(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            NativeMethods.aubio_filter_do(this, input);
        }

        [PublicAPI]
        public void DoFiltFilt([NotNull] FVec input, [NotNull] FVec temp)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (temp == null)
                throw new ArgumentNullException(nameof(temp));

            NativeMethods.aubio_filter_do_filtfilt(this, input, temp);
        }

        [PublicAPI]
        public void DoOutplace([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_filter_do_outplace(this, input, output);
        }

        [PublicAPI]
        public void DoReset()
        {
            ThrowIfDisposed();
            NativeMethods.aubio_filter_do_reset(this);
        }
    }
}