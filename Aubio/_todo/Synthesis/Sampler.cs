using System;
using Aubio.Interop;
using JetBrains.Annotations;

namespace Aubio.Synthesis
{
    /// <summary>
    ///     https://aubio.org/doc/latest/sampler_8h.html
    /// </summary>
    public sealed class Sampler : AubioObject
    {
        public Sampler(uint sampleRate, uint hopSize)
            : base(Create(sampleRate, hopSize))
        {
        }

        [PublicAPI]
        public bool IsPlaying
        {
            get
            {
                ThrowIfDisposed();
                return NativeMethods.aubio_sampler_get_playing(this);
            }
            set
            {
                ThrowIfDisposed();
                ThrowIfNot(NativeMethods.aubio_sampler_set_playing(this, value));
            }
        }

        private static IntPtr Create(uint sampleRate, uint hopSize)
        {
            return NativeMethods.new_aubio_sampler(sampleRate, hopSize);
        }

        [PublicAPI]
        public void Do([NotNull] FVec input, [NotNull] FVec output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_sampler_do(this, input, output);
        }

        [PublicAPI]
        public void DoMulti([NotNull] FMat input, [NotNull] FMat output)
        {
            ThrowIfDisposed();

            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            NativeMethods.aubio_sampler_do_multi(this, input, output);
        }

        [PublicAPI]
        public bool Load([NotNull] string uri)
        {
            ThrowIfDisposed();

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            var load = NativeMethods.aubio_sampler_load(this, uri);
            return load;
        }

        [PublicAPI]
        public bool Play()
        {
            ThrowIfDisposed();
            var play = NativeMethods.aubio_sampler_play(this);
            return play;
        }

        public bool Stop()
        {
            ThrowIfDisposed();
            var stop = NativeMethods.aubio_sampler_stop(this);
            return stop;
        }

        protected override void DisposeNative()
        {
            ThrowIfDisposed();
            NativeMethods.del_aubio_sampler(this);
        }
    }
}