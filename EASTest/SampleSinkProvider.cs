using EASLib;
using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;

namespace EASTest
{
    internal class SampleSinkProvider : SampleSink, ISampleProvider
    {
        public override int SampleRate { get; protected set; }
        public readonly List<float> Samples;
        
        public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, 1);

        private int readPosition;

        public SampleSinkProvider(int sampleRate = 96000)
        {
            SampleRate = sampleRate;
            Samples = new List<float>();
            readPosition = 0;
        }

        public void Rewind()
        {
            readPosition = 0;
        }

        public override void AppendSamples(float[] data)
        {
            Samples.AddRange(data);
        }

        public void AppendWaves(IWaveProvider waves)
        {
            using MediaFoundationResampler resampler = new MediaFoundationResampler(waves, WaveFormat);
            ISampleProvider provider = resampler.ToSampleProvider();
            float[] frame = new float[128];

            int read;
            do {
                read = provider.Read(frame, 0, frame.Length);
                Samples.AddRange(frame.Take(read));
            } while(read > 0);

            AppendSilence(1.0f);
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
    }
}
