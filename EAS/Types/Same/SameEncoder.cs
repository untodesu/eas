using EAS.Types.Enums;
using System.Text;

namespace EAS.Types.Same
{
    public static class SameEncoder
    {
        public const float BaudRate = 520.83f;
        public const float MarkFrequency = 2083.33f;
        public const float SpaceFrequency = 1562.50f;
        public const float AttentionDuration = 10.0f;

        public static readonly byte[] Preamble = new byte[16] {
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB,
            0xAB, 0xAB, 0xAB, 0xAB
        };

        public static readonly byte[] EOM = new byte[4] {
            0x4E, 0x4E, 0x4E, 0x4E
        };

        public static void WriteHeader(SampleBuffer buffer, SameHeader header, int bursts = 3)
        {
            byte[] headerData = Encoding.ASCII.GetBytes(header.ToString());
            for(int i = 0; i < bursts; i++) {
                buffer.WriteAFSK(Preamble, BaudRate, MarkFrequency, SpaceFrequency);
                buffer.WriteAFSK(headerData, BaudRate, MarkFrequency, SpaceFrequency);
                buffer.WriteSilence(1.0f);
            }
        }

        public static void WriteEOM(SampleBuffer buffer, int bursts = 3)
        {
            for(int i = 0; i < bursts; i++) {
                buffer.WriteAFSK(Preamble, BaudRate, MarkFrequency, SpaceFrequency);
                buffer.WriteAFSK(EOM, BaudRate, MarkFrequency, SpaceFrequency);
                buffer.WriteSilence(1.0f);
            }
        }

        public static void WriteAttentionSignal(SampleBuffer buffer, AttentionSignal signal)
        {
            switch(signal) {
                case AttentionSignal.WeatherRadio:
                    buffer.WriteTone(AttentionDuration, 1050.0f);
                    break;
                case AttentionSignal.BroadcastRadioOrTV:
                    buffer.WriteTone(AttentionDuration, 853.0f, 960.0f);
                    break;
                default:
                    return;
            }

            buffer.WriteSilence(1.0f);
        }
    }
}
