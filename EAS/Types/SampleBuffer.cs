using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace EAS.Types
{
    public class SampleBuffer : ISampleProvider
    {
        public readonly int SampleRate;
        public readonly List<float> Samples;

        public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, 1);

        private int readPosition;

        public SampleBuffer(int sampleRate = 96000)
        {
            SampleRate = sampleRate;
            Samples = new List<float>();
            readPosition = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int result = 0;
            readPosition += offset;
            for(int i = 0; i < count; i++) {
                if(readPosition >= Samples.Count)
                    break;
                buffer[i] = Samples[readPosition++];
                result++;
            }

            return result;
        }

        public void Rewind()
        {
            readPosition = 0;
        }

        public void WriteAFSK(byte[] data, float baud, float mark, float space)
        {
            float bitDuration = 1.0f / baud;
            for(int i = 0; i < data.Length; i++) {
                for(int j = 0; j < 8; j++) {
                    WriteTone(bitDuration, (((data[i] >> j) & 1) != 0) ? mark : space );
                }
            }
        }

        public void WriteTone(float duration, params float[] frequencies)
        {
            int durationSamples = (int)(SampleRate * duration);
            for(int i = 0; i < durationSamples; i++) {
                float arg = 2.0f * MathF.PI * (i / (float)SampleRate);
                float sample = 0.0f;

                for(int j = 0; j < frequencies.Length; j++) {
                    sample += MathF.Sin(arg * frequencies[j]);
                }

                Samples.Add(sample / frequencies.Length);
            }
        }

        public void WriteSilence(float duration)
        {
            int durationSamples = (int)(SampleRate * duration);
            for(int i = 0; i < durationSamples; i++) {
                Samples.Add(0.0f);
            }
        }

        public void WriteWaves(IWaveProvider provider)
        {
            using(MediaFoundationResampler resampler = new MediaFoundationResampler(provider, WaveFormat)) {
                float[] buffer = new float[128];
                ISampleProvider sampleProvider = resampler.ToSampleProvider();
                while(sampleProvider.Read(buffer, 0, buffer.Length) != 0) {
                    Samples.AddRange(buffer);
                }
            }
        }
    }
}
