using System;

namespace EASLib
{
    /// <summary>
    /// Responsible for receiving the PCM sample
    /// data from the library during encoding.
    /// </summary>
    public abstract class SampleSink
    {
        /// <summary>
        /// The total amount of samples enough to represent
        /// a single second of audio; measured in hertz.
        /// </summary>
        public abstract int SampleRate { get; protected set; }

        /// <summary>
        /// Called when the library needs to append a specific
        /// amount of samples to the sink.
        /// </summary>
        /// <param name="data">Input samples</param>
        public abstract void AppendSamples(float[] data);

        /// <summary>
        /// Appends a digital frequency-shift keying signal to the sample sink.
        /// Frequency-shift keying ((A)FSK) is a method of transmitting a
        /// digital data through discrete frequency changes of a signal.
        /// Each byte's bits are transmitted starting from the least significant one.
        /// </summary>
        /// <param name="data">Digital data</param>
        /// <param name="duration">Duration of a single bit in seconds</param>
        /// <param name="mark">Mark (bit is set) frequency in hertz</param>
        /// <param name="space">Space (bit is clear) frequency in hertz</param>
        public void AppendFSK(byte[] data, float duration, float mark, float space)
        {
            for(int i = 0; i < data.Length; i++) {
                for(int j = 0; j < 8; j++) {
                    bool bit = ((data[i] >> j) & 1) != 0;
                    AppendSine(duration, bit ? mark : space);
                }
            }
        }

        /// <summary>
        /// Appends silence to the sample sink.
        /// </summary>
        /// <param name="duration">Duration in seconds</param>
        public void AppendSilence(float duration)
        {
            AppendSamples(new float[(int)(SampleRate * duration)]);
        }

        /// <summary>
        /// Appends a simple sine signal to the sample sink.
        /// </summary>
        /// <param name="duration">Duration of the singnal in seconds</param>
        /// <param name="frequency">Frequency of the singnal in Hertz</param>
        public void AppendSine(float duration, float frequency)
        {
            float arg = 2.0f * MathF.PI * frequency;
            float[] samples = new float[(int)(SampleRate * duration)];
            for(int i = 0; i < samples.Length; i++)
                samples[i] = MathF.Sin(arg * (i / (float)SampleRate));
            AppendSamples(samples);
        }

        /// <summary>
        /// Appends a combination of two simple sine signals to the sample sink.
        /// </summary>
        /// <param name="duration">Duration of the singnal</param>
        /// <param name="frequency1">The first frequency of the signal in hertz</param>
        /// <param name="frequency2">The second frequuency of the signal in hertz</param>
        public void AppendTwoSines(float duration, float frequency1, float frequency2)
        {
            float arg1 = 2.0f * MathF.PI * frequency1;
            float arg2 = 2.0f * MathF.PI * frequency2;
            float[] samples = new float[(int)(SampleRate * duration)];
            for(int i = 0; i < samples.Length; i++) {
                float t = i / (float)SampleRate;
                samples[i] = 0.5f * MathF.Sin(arg1 * t) + 0.5f * MathF.Sin(arg2 * t);
            }
            AppendSamples(samples);
        }
    }
}
