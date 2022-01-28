using EASLib.Enums;
using System.Text;

namespace EASLib
{
    public static class SameEncoder
    {
        public const float BaudRate = 520.833f;
        public const float BitDuration = 1.0f / BaudRate;
        public const float MarkFrequency = 2083.333f;
        public const float SpaceFrequency = 1562.5f;
        public const float AttentionDuration = 10.0f;

        public static readonly byte[] PreambleBytes = new byte[16] {
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB
        };

        public static readonly byte[] EndOfMessageBytes = new byte[4] {
            0x4E, 0x4E, 0x4E, 0x4E
        };

        /// <summary>
        /// Appends a SAME header to the sample sink.
        /// </summary>
        /// <param name="sink">The sample sink</param>
        /// <param name="header">The SAME header</param>
        /// <param name="bursts">The amount of data bursts</param>
        public static void AppendHeader(SampleSink sink, SameHeader header, uint bursts = 3)
        {
            byte[] headerBytes = Encoding.ASCII.GetBytes(header.ToString());
            for(uint i = 0; i < bursts; i++) {
                sink.AppendFSK(PreambleBytes, BitDuration, MarkFrequency, SpaceFrequency);
                sink.AppendFSK(headerBytes, BitDuration, MarkFrequency, SpaceFrequency);
                sink.AppendSilence(1.0f);
            }
        }

        /// <summary>
        /// Appends an attention tone to the sample sink.
        /// </summary>
        /// <param name="sink">The sample sink</param>
        /// <param name="signal">The attention tone type</param>
        /// <param name="duration">Duration of the attention tone in seconds</param>
        public static void AppendAttentionSignal(SampleSink sink, AttentionSignal signal, float duration = 10.0f)
        {
            switch(signal) {
                case AttentionSignal.Weatheradio:
                    sink.AppendSine(duration, 1050.0f);
                    break;
                case AttentionSignal.RadioAndTV:
                    sink.AppendTwoSines(duration, 853.0f, 960.0f);
                    break;
            }

            sink.AppendSilence(1.0f);
        }

        /// <summary>
        /// Appends a SAME EOM (End Of Message) to the sample sink.
        /// </summary>
        /// <param name="sink">The sample sink</param>
        /// <param name="bursts">The amount of data bursts</param>
        public static void AppendEndOfMessage(SampleSink sink, uint bursts = 3)
        {
            for(uint i = 0; i < bursts; i++) {
                sink.AppendFSK(PreambleBytes, BitDuration, MarkFrequency, SpaceFrequency);
                sink.AppendFSK(EndOfMessageBytes, BitDuration, MarkFrequency, SpaceFrequency);
                sink.AppendSilence(1.0f);
            }
        }
    }
}
