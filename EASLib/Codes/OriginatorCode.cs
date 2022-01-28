namespace EASLib.Codes
{
    /// <summary>
    /// Originator is either an organization or a station
    /// which had initially triggered the EAS alarm. 
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
