using System;
using Aubio.Extensions;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Utils
{
    public sealed class Parameter : AubioObject
    {
        public Parameter(float minValue, float maxValue, int steps)
            : base(Create(minValue, maxValue, steps))
        {
        }

        [PublicAPI]
        public float Current
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_parameter_get_current_value(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_parameter_set_current_value(this, value));
            }
        }


        [PublicAPI]
        public float Max
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_parameter_get_max_value(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_parameter_set_max_value(this, value));
            }
        }

        [PublicAPI]
        public float Min
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_parameter_get_min_value(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_parameter_set_min_value(this, value));
            }
        }

        [PublicAPI]
        public int Steps
        {
            get
            {
                ThrowIfDisposed();
                var value = (int) NativeMethods.aubio_parameter_get_steps(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();

                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                ThrowIfNot(NativeMethods.aubio_parameter_set_steps(this, value.ToUInt32()));
            }
        }

        private static IntPtr Create(float minValue, float maxValue, int steps)
        {
            if (steps <= 0)
                throw new ArgumentOutOfRangeException(nameof(steps));

            var handle = NativeMethods.new_aubio_parameter(minValue, maxValue, steps.ToUInt32());

            return handle;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_parameter(this);
        }

        [PublicAPI]
        public float GetNextValue()
        {
            ThrowIfDisposed();
            var value = NativeMethods.aubio_parameter_get_next_value(this);
            return value;
        }

        [PublicAPI]
        public bool SetTargetValue(float value)
        {
            ThrowIfDisposed();
            var result = NativeMethods.aubio_parameter_set_target_value(this, value);
            return result;
        }
    }
}