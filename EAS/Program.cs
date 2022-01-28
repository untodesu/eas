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

            SameHeader header = new SameHeader(OriginatorCode.EASParticipant,
                EventCode.CivilEmergencyMessage, new TimeSpan(1, 20, 0), "AMONG/US");

            header.Locations.Add(new LocationCode(12, 0));
            header.Locations.Add(new LocationCode(35, 0));
            header.Locations.Add(new LocationCode(48, 0));

            SameEncoder.WriteHeader(buffer, header);
            SameEncoder.WriteAttentionSignal(buffer, AttentionSignal.BroadcastRadioOrTV);
            using(Mp3FileReader reader = new Mp3FileReader("tts.mp3")) {
                buffer.WriteWaves(reader);
                buffer.WriteSilence(1.0f);
            }
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
