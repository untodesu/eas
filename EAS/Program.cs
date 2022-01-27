using NAudio.Wave;
using System.Threading;
using System;
using EAS.Types;
using EAS.Types.Codes;
using EAS.Types.Enums;
using EAS.Types.Same;

namespace EAS
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            SampleBuffer buffer = new SampleBuffer(96000);

            SameHeader header = new SameHeader(OriginatorCode.EASParticipant, EventCode.RequiredMonthlyTest, new TimeSpan(0, 15, 0), "WABC/FM");
            header.Locations.Add(new LocationCode(35, 0));
            header.Locations.Add(new LocationCode(48, 0));

            SameEncoder.WriteHeader(buffer, header);
            SameEncoder.WriteAttentionSignal(buffer, AttentionSignal.BroadcastRadioOrTV);
            SameEncoder.WriteEOM(buffer);

            Console.WriteLine(header.ToString());

            using(WaveFileWriter writer = new WaveFileWriter("eas.wav", buffer.WaveFormat)) {
                writer.WriteSamples(buffer.Samples.ToArray(), 0, buffer.Samples.Count);
            }
            
            using(WaveOutEvent dev = new WaveOutEvent()) {
                dev.Init(buffer);
                dev.Play();
                while(dev.PlaybackState == PlaybackState.Playing)
                    Thread.Sleep(1);
            }
        }
    }
}
