using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Detection
{
    /// <summary>
    ///     https://aubio.org/doc/latest/pitch_8h.html
    /// </summary>
    public sealed class Pitch : AubioObject
    {
        public Pitch([NotNull] string method, uint bufferSize, uint hopSize, uint sampleRate)
            : base(Create(method, bufferSize, hopSize, sampleRate))
        {
        }

        [PublicAPI]
        public float Confidence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_pitch_get_confidence(this);
                return value;
            }
        }

        [PublicAPI]
        public float Silence
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_pitch_get_silence(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_pitch_set_silence(this, value));
            }
        }

        [PublicAPI]
        public float Tolerance
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_pitch_get_tolerance(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_pitch_set_tolerance(this, value));
            }
        }

        private static IntPtr Create(string method, uint bufferSize, uint hopSize, uint sampleRate)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            return Create(NativeMethods.new_aubio_pitch(method, bufferSize, hopSize, sampleRate));
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_pitch(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_pitch_do(this, input, output);
        }

        [PublicAPI]
        public void SetUnit([NotNull] string mode)
        {
            ThrowIfDisposed();

            if (mode == null)
                throw new ArgumentNullException(nameof(mode));

            ThrowIfNot(NativeMethods.aubio_pitch_set_unit(this, mode));
        }
    }
}