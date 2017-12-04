using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Aubio.NET.Vectors;
using JetBrains.Annotations;

namespace Aubio.NET.Spectral
{
    /// <summary>
    ///     https://aubio.org/doc/latest/phasevoc_8h.html
    /// </summary>
    public sealed class PhaseVocoder : AubioObject
    {
        #region Fields

        [PublicAPI]
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly unsafe PhaseVocoder__* Handle;

        #endregion

        #region Public Members

        [PublicAPI]
        public unsafe PhaseVocoder(int windowSize = 1024, int hopSize = 256)
        {
            if (windowSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(windowSize));

            if (hopSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(hopSize));

            var handle = new_aubio_pvoc((uint) windowSize, (uint) hopSize);
            if (handle == null)
                throw new ArgumentNullException(nameof(handle));

            Handle = handle;
        }

        [PublicAPI]
        public unsafe int HopSize => (int) aubio_pvoc_get_hop(Handle);

        [PublicAPI]
        public unsafe int WindowSize => (int) aubio_pvoc_get_win(Handle);

        [PublicAPI]
        public unsafe void AddSynth([NotNull] FVec synth)
        {
            if (synth == null)
                throw new ArgumentNullException(nameof(synth));

            aubio_pvoc_addsynth(Handle, synth.Handle);
        }

        [PublicAPI]
        public unsafe void Do([NotNull] FVec input, [NotNull] CVec fftGrain)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            aubio_pvoc_do(Handle, input.Handle, fftGrain.Handle);
        }

        [PublicAPI]
        public unsafe void Rdo([NotNull] CVec fftGrain, [NotNull] FVec output)
        {
            if (fftGrain == null)
                throw new ArgumentNullException(nameof(fftGrain));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            aubio_pvoc_rdo(Handle, fftGrain.Handle, output.Handle);
        }

        [PublicAPI]
        public unsafe void SetWindow(FVecWindowType windowType)
        {
            var attribute = windowType.GetDescriptionAttribute();
            var window = attribute.Description;

            if (aubio_pvoc_set_window(Handle, window))
                throw new ArgumentOutOfRangeException(nameof(windowType));
        }

        [PublicAPI]
        public unsafe void SwapBuffers([NotNull] FVec buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            aubio_pvoc_swapbuffers(Handle, buffer.Handle);
        }

        #endregion

        #region Overrides of AubioObject

        protected override unsafe void DisposeNative()
        {
            del_aubio_pvoc(Handle);
        }

        #endregion

        #region Native Methods

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe PhaseVocoder__* new_aubio_pvoc(
            uint windowSize,
            uint hopSize
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void del_aubio_pvoc(
            PhaseVocoder__* vocoder
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_pvoc_do(
            PhaseVocoder__* vocoder,
            FVec__* input,
            CVec__* fftGrain
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_pvoc_rdo(
            PhaseVocoder__* vocoder,
            CVec__* fftGrain,
            FVec__* output
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_pvoc_get_win(
            PhaseVocoder__* vocoder
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe uint aubio_pvoc_get_hop(
            PhaseVocoder__* vocoder
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern unsafe bool aubio_pvoc_set_window(
            PhaseVocoder__* vocoder,
            [MarshalAs(UnmanagedType.LPStr)] string window
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_pvoc_swapbuffers(
            PhaseVocoder__* vocoder,
            FVec__* buffer
        );

        [SuppressUnmanagedCodeSecurity]
        [DllImport("aubio", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe void aubio_pvoc_addsynth(
            PhaseVocoder__* vocoder,
            FVec__* synth
        );

        #endregion
    }
}