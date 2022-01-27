namespace EAS.Types.Codes
{
    /// <summary>
    /// Originator code indicates the initial
    /// source of the EAS system activation.
    /// </summary>
    public class OriginatorCode
    {
        public readonly string Code;

        private OriginatorCode(string code)
        {
            Code = code;
        }

        public static readonly OriginatorCode PrimaryEntryPoint = new OriginatorCode("PEP");
        public static readonly OriginatorCode CivilAuthorities = new OriginatorCode("CIV");
        public static readonly OriginatorCode NationalWeatherService = new OriginatorCode("WXR");
        public static readonly OriginatorCode EASParticipant = new OriginatorCode("EAS");
        public static readonly OriginatorCode EmergencyActionNotification = new OriginatorCode("EAN");
        public static readonly OriginatorCode FakeOriginator = new OriginatorCode("ABC");
    }
}
