using System;
using System.Runtime.InteropServices;
using Aubio.Detection;
using Aubio.Utils;
using JetBrains.Annotations;

namespace Aubio.Interop
{
    internal static class NativeMethods
    {
        [PublicAPI]
        public const string DllName = "libaubio-6.dll";

        private const CallingConvention Convention = CallingConvention.Cdecl;

        #region source

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_source_close(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_source_do(IntPtr handle, IntPtr buffer, out uint read);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_source_do_multi(IntPtr handle, IntPtr buffer, out uint read);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_source_get_channels(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_source_get_duration(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_source_get_samplerate(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_source_seek(IntPtr handle, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_source(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_source(uint sampleRate, uint hopSize);

        #endregion

        #region sink

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sink_close(IntPtr handle);


        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_sink_do(IntPtr handle, IntPtr buffer, out uint written);


        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_sink_do_multi(IntPtr handle, IntPtr buffer, out uint written);


        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_sink_get_channels(IntPtr handle);


        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_sink_get_samplerate(IntPtr handle);


        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sink_preset_channels(IntPtr handle, uint channels);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sink_preset_samplerate(IntPtr handle, uint sampleRate);


        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_sink(IntPtr handle);


        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        public static extern IntPtr new_aubio_sink(string uri, uint sampleRate);

        #endregion


        #region fft

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_do(IntPtr handle, IntPtr input, IntPtr spectrum);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_do_complex(IntPtr handle, IntPtr input, IntPtr compspec);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_imag(IntPtr spectrum, IntPtr compspec);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_norm(IntPtr compspec, IntPtr spectrum);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_phas(IntPtr compspec, IntPtr spectrum);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_real(IntPtr spectrum, IntPtr compspec);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_realimag(IntPtr spectrum, IntPtr compspec);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_get_spectrum(IntPtr compspec, IntPtr spectrum);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_rdo(IntPtr handle, IntPtr spectrum, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_fft_rdo_complex(IntPtr handle, IntPtr compspec, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_fft(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_fft(uint size);

        #endregion

        #region awhitening

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_spectral_whitening_do(IntPtr handle, IntPtr fftGrain);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_spectral_whitening(uint bufferSize, uint hopSize, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_spectral_whitening_reset(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_spectral_whitening_get_floor(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_spectral_whitening_get_relax_time(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_spectral_whitening_set_floor(IntPtr handle, float floor);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_spectral_whitening_set_relax_time(IntPtr handle, float relaxTime);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_spectral_whitening(IntPtr handle);

        #endregion

        #region filterbank

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_filterbank(uint nFilters, uint windowSize);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_filterbank(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_filterbank_do(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr aubio_filterbank_get_coeffs(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filterbank_set_coeffs(IntPtr handle, IntPtr filters);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filterbank_set_mel_coeffs_slaney(IntPtr handle, float sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filterbank_set_triangle_bands(IntPtr handle, IntPtr frequencies,
            float sampleRate);

        #endregion

        #region mfcc

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_mfcc(uint bufferSize, uint nFilters, uint nCoeffs, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_mfcc(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_mfcc_do(IntPtr handle, IntPtr input, IntPtr output);

        #endregion

        #region phasevoc

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_pvoc(uint windowSize, uint hopSize);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_pvoc(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_pvoc_do(IntPtr handle, IntPtr input, IntPtr fftGrain);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_pvoc_rdo(IntPtr handle, IntPtr fftGrain, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_pvoc_get_win(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_pvoc_get_hop(IntPtr handle);

        #endregion

        #region specdesc

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_specdesc_do(IntPtr handle, IntPtr fftgrain, IntPtr desc);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        public static extern IntPtr new_aubio_specdesc(string method, uint bufferSize);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_specdesc(IntPtr handle);

        #endregion

        #region TSS

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_tss(uint bufferSize, uint hopSize);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_tss(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_tss_do(IntPtr handle, IntPtr input, IntPtr trans, IntPtr stead);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tss_set_threshold(IntPtr handle, float threshold);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tss_set_alpha(IntPtr handle, float alpha);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tss_set_beta(IntPtr handle, float beta);

        #endregion

        #region Parameter

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_parameter(float minValue, float maxValue, uint steps);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_parameter_set_target_value(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_parameter_get_next_value(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_parameter_get_current_value(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_parameter_set_current_value(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_parameter_set_max_value(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_parameter_set_min_value(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_parameter_set_steps(IntPtr handle, uint steps);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_parameter_get_max_value(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_parameter_get_min_value(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_parameter_get_steps(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_parameter(IntPtr handle);

        #endregion

        #region Log

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_log_reset();

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_log_set_function(AubioLogFunction function, IntPtr data);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern AubioLogFunction aubio_log_set_level_function(
            AubioLogLevel level, AubioLogFunction function, IntPtr handle);

        #endregion

        #region Notes

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_notes(string method, uint bufferSize, uint hopSize, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_notes(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_notes_do(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_notes_set_silence(IntPtr handle, float silence);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_notes_get_silence(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_notes_get_minioi_ms(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_notes_set_minioi_ms(IntPtr handle, float value);

        #endregion

        #region Resampler

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_resampler(float ratio, uint type);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_resampler(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_resampler_do(IntPtr handle, IntPtr input, IntPtr output);

        #endregion

        #region Onset

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_onset(string method, uint bufferSize, uint hopSize, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_onset_do(IntPtr handle, IntPtr input, IntPtr onset);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_onset_get_last(IntPtr handle);

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_last_s(IntPtr handle);
        */

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_last_ms(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_awhitening(IntPtr handle,
            [MarshalAs(UnmanagedType.Bool)] bool enable);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_get_awhitening(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_compression(IntPtr handle, float lambda);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_onset_get_compression(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_silence(IntPtr handle, float silence);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_onset_get_silence(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_onset_get_descriptor(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_onset_get_thresholded_descriptor(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_threshold(IntPtr handle, float threshold);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_minioi(IntPtr handle, uint minioi);

        /*
                [DllImport(DllName)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_onset_set_minioi_s(IntPtr handle, float minioi);
        */

        /*
                [DllImport(DllName)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_onset_set_minioi_ms(IntPtr handle, float minioi);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_delay(IntPtr handle, uint delay);

        /*
                [DllImport(DllName)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_onset_set_delay_s(IntPtr handle, float delay);
        */

        /*
                [DllImport(DllName)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_onset_set_delay_ms(IntPtr handle, float delay);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_onset_get_minioi(IntPtr handle);

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_minioi_s(IntPtr handle);
        */

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_minioi_ms(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_onset_get_delay(IntPtr handle);

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_delay_s(IntPtr handle);
        */

        /*
                [DllImport(DllName)]
                public static extern float aubio_onset_get_delay_ms(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_onset_get_threshold(IntPtr handle);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_onset_set_default_parameters(IntPtr handle, string mode);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_onset_reset(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_onset(IntPtr handle);

        #endregion

        #region CVec

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_cvec(uint length);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_cvec(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_norm_set_sample(IntPtr handle, float val, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_phas_set_sample(IntPtr handle, float val, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float cvec_norm_get_sample(IntPtr handle, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float cvec_phas_get_sample(IntPtr handle, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr cvec_norm_get_data(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr cvec_phas_get_data(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_print(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_copy(IntPtr handle, IntPtr target);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_norm_set_all(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_norm_zeros(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_norm_ones(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_phas_set_all(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_phas_zeros(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_phas_ones(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_zeros(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void cvec_logmag(IntPtr handle, float lambda);

        #endregion

        #region FVec

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_fvec(uint length);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_fvec(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float fvec_get_sample(IntPtr ptr, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_set_sample(IntPtr ptr, float value, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr fvec_get_data(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_print(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_set_all(IntPtr ptr, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_zeros(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_ones(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_rev(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_copy(IntPtr ptr, IntPtr target);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_weighted_copy(IntPtr ptr, IntPtr weight, IntPtr target);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_exp(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_cos(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_sin(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_abs(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_sqrt(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_log10(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_log(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_floor(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_ceil(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_round(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_pow(IntPtr handle, float pow);

        #endregion

        #region LVec

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_lvec(uint length);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_lvec(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern double lvec_get_sample(IntPtr ptr, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void lvec_set_sample(IntPtr ptr, double value, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr lvec_get_data(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void lvec_print(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void lvec_set_all(IntPtr ptr, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void lvec_zeros(IntPtr ptr);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void lvec_ones(IntPtr ptr);

        #endregion

        #region Pitch

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_pitch_do(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_pitch_set_tolerance(IntPtr handle, float tolerance);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_pitch_get_tolerance(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_pitch(IntPtr handle);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_pitch(string method, uint bufferSize, uint hopSize, uint sampleRate);

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_pitch_set_unit(IntPtr handle, string mode);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_pitch_set_silence(IntPtr handle, float silence);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_pitch_get_silence(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_pitch_get_confidence(IntPtr handle);

        #endregion

        #region Tempo

        [DllImport(DllName, CharSet = CharSet.Ansi, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_tempo(string method, uint bufferSize, uint hopSize, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_tempo_do(IntPtr handle, IntPtr input, IntPtr tempo);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_tempo_get_last(IntPtr handle);

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                public static extern float aubio_tempo_get_last_s(IntPtr handle);
        */

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                public static extern float aubio_tempo_get_last_ms(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tempo_set_silence(IntPtr handle, float silence);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_silence(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tempo_set_threshold(IntPtr handle, float threshold);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_threshold(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_period(IntPtr handle);

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                public static extern float aubio_tempo_get_period_s(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_bpm(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_confidence(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tempo_set_tatum_signature(IntPtr handle, uint signature);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern Tempo.Tatum aubio_tempo_was_tatum(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_tempo_get_last_tatum(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern int aubio_tempo_get_delay(IntPtr handle);

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                public static extern float aubio_tempo_get_delay_s(IntPtr handle);
        */

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                public static extern float aubio_tempo_get_delay_ms(IntPtr handle);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_tempo_set_delay(IntPtr handle, int delay);

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_tempo_set_delay_s(IntPtr handle, float delay);
        */

        /*
                [DllImport(DllName, CallingConvention = Convention.Cdecl)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool aubio_tempo_set_delay_ms(IntPtr handle, float delay);
        */

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_tempo(IntPtr handle);

        #endregion

        #region Sampler

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_sampler_do(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_sampler_do_multi(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sampler_get_playing(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sampler_load(IntPtr handle, string uri);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sampler_play(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool
            aubio_sampler_set_playing(IntPtr handle, [MarshalAs(UnmanagedType.Bool)] bool playing);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_sampler_stop(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_sampler(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_sampler(uint sampleRate, uint hopSize);

        #endregion

        #region Wavetable

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_wavetable_do(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_wavetable_do_multi(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_wavetable_get_amp(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_wavetable_get_freq(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_get_playing(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_load(IntPtr handle, string uri);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_play(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_set_amp(IntPtr handle, float amplitude);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_set_freq(IntPtr handle, float frequency);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_set_playing(IntPtr handle,
            [MarshalAs(UnmanagedType.Bool)] bool playing);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_wavetable_stop(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_wavetable(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_wavetable(uint sampleRate, uint hopSize);

        #endregion

        #region FMat

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_fmat(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_copy(IntPtr handle, IntPtr target);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_get_channel(IntPtr handle, uint channel, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr fmat_get_channel_data(IntPtr handle, uint channel);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr fmat_get_data(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float fmat_get_sample(IntPtr handle, uint channel, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_ones(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_print(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_rev(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_set(IntPtr handle, float value);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_set_sample(IntPtr handle, float value, uint channel, uint position);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_vecmul(IntPtr handle, IntPtr scale, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_weight(IntPtr handle, IntPtr weight);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fmat_zeros(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_fmat(uint height, uint length);

        #endregion

        #region Window

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool fvec_set_window(IntPtr handle, string windowType);

        [DllImport(DllName, CallingConvention = Convention, CharSet = CharSet.Ansi)]
        public static extern IntPtr new_aubio_window(string windowType, uint length);

        #endregion

        #region MusicUtils

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_cleanup();

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_db_spl(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_level_detection(IntPtr handle, float threshold);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_level_lin(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_silence_detection(IntPtr handle, float threshold);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_unwrap2pi(float phase);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern float aubio_zero_crossing_rate(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void fvec_clamp(IntPtr handle, float absmax);

        #endregion

        #region Filters

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filter_set_a_weighting(IntPtr handle, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filter_set_biquad(IntPtr handle,
            double b0, double b1, double b2, double a1, double a2);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filter_set_c_weighting(IntPtr handle, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_filter_a_weighting(uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_filter_biquad(double b0, double b1, double b2, double a1, double a2);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_filter_c_weighting(uint sampleRate);

        #endregion

        #region Filter

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_filter_do(IntPtr handle, IntPtr input);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_filter_do_filtfilt(IntPtr handle, IntPtr input, IntPtr temp);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_filter_do_outplace(IntPtr handle, IntPtr input, IntPtr output);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void aubio_filter_do_reset(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr aubio_filter_get_feedback(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr aubio_filter_get_feedforward(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_filter_get_order(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern uint aubio_filter_get_samplerate(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool aubio_filter_set_samplerate(IntPtr handle, uint sampleRate);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern void del_aubio_filter(IntPtr handle);

        [DllImport(DllName, CallingConvention = Convention)]
        public static extern IntPtr new_aubio_filter(uint order);

        #endregion
    }
}