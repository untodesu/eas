using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASLib.Codes;

namespace EASLib
{
    public class SameHeader
    {
        public OriginatorCode Originator { get; set; }
        public EventCode Cause { get; set; }
        public List<LocationCode> AffectedLocations { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime TriggerDateTimeUTC { get; set; }
        public string StationCallsign { get; set; }

        public SameHeader(params LocationCode[] affectedLocations)
        {
            Originator = OriginatorCode.FakeOriginator;
            Cause = EventCode.RequiredMonthlyTest;
            AffectedLocations = new List<LocationCode>(affectedLocations);
            Duration = new TimeSpan(0, 15, 0);
            TriggerDateTimeUTC = DateTime.UtcNow;
            StationCallsign = "FAKE/ASF";
        }

        public SameHeader(OriginatorCode originator, EventCode cause, TimeSpan duration, string stationCallsign, params LocationCode[] affectedLocations)
        {
            Originator = originator;
            Cause = cause;
            AffectedLocations = new List<LocationCode>(affectedLocations);
            Duration = duration;
            TriggerDateTimeUTC = DateTime.UtcNow;
            StationCallsign = stationCallsign.Replace('-', '/');
        }

        public SameHeader(OriginatorCode originator, EventCode cause, TimeSpan duration, DateTime triggerDateTime, string stationCallsign, params LocationCode[] affectedLocations)
        {
            Originator = originator;
            Cause = cause;
            AffectedLocations = new List<LocationCode>(affectedLocations);
            Duration = duration;
            TriggerDateTimeUTC = triggerDateTime.ToUniversalTime();
            StationCallsign = stationCallsign.Replace('-', '/');
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("ZCZC-");

            builder.AppendFormat("{0}-", Originator.Code);
            builder.AppendFormat("{0}-", Cause.Code);

            if(AffectedLocations.Count == 0) {
                throw new ArgumentOutOfRangeException("At least one location code is required!");
            }

            // Locations are separated between themselves
            // by a dash; however, expiration time starts
            // with a plus (to indicate that we are done
            // with the list of affected locations).
            for(int i = 0; i < AffectedLocations.Count; i++) {
                char separator = '-';
                if(i == (AffectedLocations.Count - 1))
                    separator = '+';
                builder.AppendFormat("{0}{1}", AffectedLocations[i].Code, separator);
            }

            // Expiration time minutes are 15-aligned if
            // total expiration time is less than an hour.
            // Otherwise minutes are 30-aligned.
            int align = (Duration.Hours != 0) ? 30 : 15;
            int rem = Duration.Minutes % align;
            builder.AppendFormat("{0:D2}{1:D2}-", Duration.Hours, Duration.Minutes + ((rem != 0) ? (align - rem) : 0));

            builder.AppendFormat("{0:D3}{1:D2}{2:D2}-", TriggerDateTimeUTC.DayOfYear, TriggerDateTimeUTC.Hour, TriggerDateTimeUTC.Minute);
            builder.AppendFormat("{0}-", StationCallsign);

            return builder.ToString().ToUpperInvariant();
        }
    }
}
