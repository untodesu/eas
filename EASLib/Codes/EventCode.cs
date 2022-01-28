namespace EASLib.Codes
{
    /// <summary>
    /// Event codes determine the cause
    /// of the triggered EAS alarm.
    /// </summary>
    public class EventCode
    {
        public readonly string Code;

        internal EventCode(string code)
        {
            Code = code;
        }

        public static readonly EventCode EmergencyActionNotification = new EventCode("EAN");
        public static readonly EventCode NationalInformationCenter = new EventCode("NIC");
        public static readonly EventCode NationalPeriodicTest = new EventCode("NPT");
        public static readonly EventCode RequiredMonthlyTest = new EventCode("RMT");
        public static readonly EventCode RequiredWeeklyTest = new EventCode("RWT");

        public static readonly EventCode AdministrativeMessage = new EventCode("ADR");
        public static readonly EventCode AvalancheWarning = new EventCode("AVW");
        public static readonly EventCode AvalancheWatch = new EventCode("AVA");
        public static readonly EventCode BlizzardWarning = new EventCode("BZW");
        public static readonly EventCode BlueAlert = new EventCode("BLU");
        public static readonly EventCode ChildAbductionEmergency = new EventCode("CAE");
        public static readonly EventCode CivilDangerWarning = new EventCode("CDW");
        public static readonly EventCode CivilEmergencyMessage = new EventCode("CEM");
        public static readonly EventCode CoastalFloodWarning = new EventCode("CFW");
        public static readonly EventCode CoastalFloodWatch = new EventCode("CFA");
        public static readonly EventCode DustStormWarning = new EventCode("DSW");
        public static readonly EventCode EarthquakeWarning = new EventCode("EQW");
        public static readonly EventCode EvacuationImmediate = new EventCode("EVI");
        public static readonly EventCode ExtremeWindWarning = new EventCode("EWW");
        public static readonly EventCode FireWarning = new EventCode("FRW");
        public static readonly EventCode FlashFloodWarning = new EventCode("FFW");
        public static readonly EventCode FlashFloodWatch = new EventCode("FFA");
        public static readonly EventCode FlashFloodStatement = new EventCode("FFS");
        public static readonly EventCode FloodWarning = new EventCode("FLW");
        public static readonly EventCode FloodWatch = new EventCode("FLA");
        public static readonly EventCode FloodStatement = new EventCode("FLS");
        public static readonly EventCode HazardousMaterialsWarning = new EventCode("HMW");
        public static readonly EventCode HighWindWarning = new EventCode("HWW");
        public static readonly EventCode HighWindWatch = new EventCode("HWA");
        public static readonly EventCode HurricaneWarning = new EventCode("HUW");
        public static readonly EventCode HurricaneWatch = new EventCode("HUA");
        public static readonly EventCode HurricaneStatement = new EventCode("HLS");
        public static readonly EventCode LawEnforcementWarning = new EventCode("LEW");
        public static readonly EventCode LocalAreaEmergency = new EventCode("LAE");
        public static readonly EventCode NetworkMessageNotification = new EventCode("NMN");
        public static readonly EventCode NiveElevenTelephoneOutageEmergency = new EventCode("TOE");
        public static readonly EventCode NuclearPowerPlantWarning = new EventCode("NUW");
        public static readonly EventCode PracticeDemoWarning = new EventCode("DMO");
        public static readonly EventCode RadiologicalHazardWarning = new EventCode("RHW");
        public static readonly EventCode SevereThunderstormWarning = new EventCode("SVR");
        public static readonly EventCode SevereThunderstormWatch = new EventCode("SVA");
        public static readonly EventCode SevereWeatherStatement = new EventCode("SVS");
        public static readonly EventCode ShelterinPlaceWarning = new EventCode("SPW");
        public static readonly EventCode SpecialMarineWarning = new EventCode("SMW");
        public static readonly EventCode SpecialWeatherStatement = new EventCode("SPS");
        public static readonly EventCode StormSurgeWatch = new EventCode("SSA");
        public static readonly EventCode StormSurgeWarning = new EventCode("SSW");
        public static readonly EventCode TornadoWarning = new EventCode("TOR");
        public static readonly EventCode TornadoWatch = new EventCode("TOA");
        public static readonly EventCode TropicalStormWarning = new EventCode("TRW");
        public static readonly EventCode TropicalStormWatch = new EventCode("TRA");
        public static readonly EventCode TsunamiWarning = new EventCode("TSW");
        public static readonly EventCode TsunamiWatch = new EventCode("TSA");
        public static readonly EventCode VolcanoWarning = new EventCode("VOW");
        public static readonly EventCode WinterStormWarning = new EventCode("WSW");
        public static readonly EventCode WinterStormWatch = new EventCode("WSA");
    }
}
