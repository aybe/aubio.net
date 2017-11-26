using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Aubio.NET.Utilities
{
    public sealed class Parameter : AubioObject
    {
        #region Fields 

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly unsafe Parameter__* _parameter;

        #endregion

        #region Constructors

        public unsafe Parameter(float minValue, float maxValue, int steps)
        {
            var parameter = new_aubio_parameter(minValue, maxValue, steps.ToUInt32());
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            _parameter = parameter;
        }

        #endregion

        #region Public Members

        [PublicAPI]
        public float Current
        {
            get => aubio_parameter_get_current_value(this);
            set
            {
                if (aubio_parameter_set_current_value(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Min
        {
            get => aubio_parameter_get_min_value(this);
            set
            {
                if (aubio_parameter_set_min_value(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float Max
        {
            get => aubio_parameter_get_max_value(this);
            set
            {
                if (aubio_parameter_set_max_value(this, value))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public int Steps
        {
            get => aubio_parameter_get_steps(this).ToInt32();
            set
            {
                if (aubio_parameter_set_steps(this, value.ToUInt32()))
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        [PublicAPI]
        public float GetNextValue()
        {
            return aubio_parameter_get_next_value(this);
        }

        [PublicAPI]
        public bool SetTargetValue(float target)
        {
            return !aubio_parameter_set_target_value(this, target);
        }

        #endregion

        #region AubioObject Members

        protected override void DisposeNative()
        {
            del_aubio_parameter(this);
        }

        internal override unsafe IntPtr ToPointer()
        {
            return new IntPtr(_parameter);
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
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_parameter_set_target_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_parameter_get_next_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_parameter_get_current_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_parameter_set_current_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_parameter_set_max_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_parameter_set_min_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter,
            float value
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool aubio_parameter_set_steps(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter,
            uint steps
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_parameter_get_max_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern float aubio_parameter_get_min_value(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint aubio_parameter_get_steps(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern void del_aubio_parameter(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AubioObjectMarshaler))]
            Parameter parameter
        );

        #endregion
    }
}