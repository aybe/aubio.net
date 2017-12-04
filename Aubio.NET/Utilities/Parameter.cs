using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Utilities
{
    /// <summary>
    ///     https://aubio.org/doc/latest/parameter_8h.html
    /// </summary>
    public sealed class Parameter : AubioObject
    {
        #region Fields 

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe Parameter__* Handle;

        #endregion

        #region Public Members

        public unsafe Parameter(float minValue, float maxValue, int steps)
        {
            if (steps <= 0)
                throw new ArgumentOutOfRangeException(nameof(steps));

            var handle = new_aubio_parameter(minValue, maxValue, (uint) steps);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe float Current
        {
            get => aubio_parameter_get_current_value(Handle);
            set
            {
                if (aubio_parameter_set_current_value(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float Min
        {
            get => aubio_parameter_get_min_value(Handle);
            set
            {
                if (aubio_parameter_set_min_value(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float Max
        {
            get => aubio_parameter_get_max_value(Handle);
            set
            {
                if (aubio_parameter_set_max_value(Handle, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe int Steps
        {
            get => (int) aubio_parameter_get_steps(Handle);
            set
            {
                if (aubio_parameter_set_steps(Handle, (uint) value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public unsafe float GetNextValue()
        {
            return aubio_parameter_get_next_value(Handle);
        }

        [PublicAPI]
        public unsafe bool SetTargetValue(float target)
        {
            return !aubio_parameter_set_target_value(Handle, target);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_parameter(Handle);
        }

        #endregion

        #region Native Methods

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe Parameter__* new_aubio_parameter(
            float minValue,
            float maxValue,
            uint steps
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_parameter(
            Parameter__* parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_parameter_set_target_value(
            Parameter__* parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_parameter_get_next_value(
            Parameter__* parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_parameter_get_current_value(
            Parameter__* parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_parameter_set_current_value(
            Parameter__* parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_parameter_set_max_value(
            Parameter__* parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_parameter_set_min_value(
            Parameter__* parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_parameter_set_steps(
            Parameter__* parameter,
            uint steps
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_parameter_get_max_value(
            Parameter__* parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe float aubio_parameter_get_min_value(
            Parameter__* parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_parameter_get_steps(
            Parameter__* parameter
        );

        #endregion
    }
}