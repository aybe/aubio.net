using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Synthesis
{
    /// <summary>
    ///     https://aubio.org/doc/latest/wavetable_8h.html
    /// </summary>
    public sealed class Wavetable : AubioObject
    {
        public Wavetable(uint sampleRate, uint hopSize)
            : base(Create(sampleRate, hopSize))
        {
        }

        [PublicAPI]
        public float Amplitude
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_wavetable_get_amp(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_wavetable_set_amp(this, value));
            }
        }

        [PublicAPI]
        public float Frequency
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_wavetable_get_freq(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_wavetable_set_freq(this, value));
            }
        }

        [PublicAPI]
        public bool IsPlaying
        {
            get
            {
                ThrowIfDisposed();
                var value = NativeMethods.aubio_wavetable_get_playing(this);
                return value;
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_wavetable_set_playing(this, value));
            }
        }

        private static IntPtr Create(uint sampleRate, uint hopSize)
        {
            return NativeMethods.new_aubio_wavetable(sampleRate, hopSize);
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_wavetable(this);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_wavetable_do(this, input, output);
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_wavetable_do_multi(this, input, output);
        }

        [PublicAPI]
        public bool Load([NotNull] string uri)
        {
            ThrowIfDisposed();

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            var load = NativeMethods.aubio_wavetable_load(this, uri);
            return load;
        }

        [PublicAPI]
        public bool Play()
        {
            ThrowIfDisposed();
            var play = NativeMethods.aubio_wavetable_play(this);
            return play;
        }

        [PublicAPI]
        public bool Stop()
        {
            ThrowIfDisposed();
            var stop = NativeMethods.aubio_wavetable_stop(this);
            return stop;
        }
    }
}