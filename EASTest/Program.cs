using EASLib;
using EASLib.Codes;
using EASLib.Enums;
using NAudio.Wave;
using System;
using System.Threading;

namespace EASTest
{
    class Program
    {
        static void Main()
        {
            SampleSinkProvider sink = new SampleSinkProvider(96000);

            SameHeader header = new SameHeader() {
                Originator = OriginatorCode.PrimaryEntryPoint,
                Cause = EventCode.EmergencyActionNotification,
                Duration = new TimeSpan(0, 15, 0)
            };

            // Entire United States
            header.AffectedLocations.Add(new LocationCode(0, 0));

            Console.WriteLine(header);

            SameEncoder.AppendHeader(sink, header);
            SameEncoder.AppendAttentionSignal(sink, AttentionSignal.RadioAndTV);
            SameEncoder.AppendEndOfMessage(sink);

            using(WaveFileWriter writer = new WaveFileWriter("../../../eastest.wav", sink.WaveFormat)) {
                Console.WriteLine("Writing {0}", writer.Filename);
                writer.WriteSamples(sink.Samples.ToArray(), 0, sink.Samples.Count);
            }

            using(WaveOutEvent waveOut = new WaveOutEvent()) {
                waveOut.Init(sink);
                waveOut.Play();
                while(waveOut.PlaybackState == PlaybackState.Playing) {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
