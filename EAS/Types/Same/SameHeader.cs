using System;
using System.Collections.Generic;
using System.Text;
using EAS.Types.Codes;

namespace EAS.Types.Same
{
    public class SameHeader
    {
        public readonly OriginatorCode Originator;
        public readonly EventCode Cause;
        public readonly TimeSpan Duration;
        public readonly DateTime TriggerTime;
        public readonly string Callsign;
        public readonly List<LocationCode> Locations;

        public SameHeader(OriginatorCode originator, EventCode cause, TimeSpan duration, DateTime triggerTime, string callsign, params LocationCode[] locations)
        {
            Originator = originator;
            Cause = cause;
            Duration = duration;
            TriggerTime = triggerTime.ToUniversalTime();
            Callsign = callsign.Replace('-', '/');
            Locations = new List<LocationCode>(locations);
        }

        public SameHeader(OriginatorCode originator, EventCode cause, TimeSpan duration, string callsign, params LocationCode[] locations)
        {
            Originator = originator;
            Cause = cause;
            Duration = duration;
            TriggerTime = DateTime.UtcNow;
            Callsign = callsign.Replace('-', '/');
            Locations = new List<LocationCode>(locations);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("ZCZC-");

            builder.AppendFormat("{0}-", Originator.Code);
            builder.AppendFormat("{0}-", Cause.Code);

            if(Locations.Count == 0) {
                throw new ArgumentOutOfRangeException("At least one location code is required!");
            }

            // Locations are separated between themselves
            // by a dash; however, expiration time starts
            // with a plus (to indicate that we are done
            // with the list of affected locations).
            for(int i = 0; i < Locations.Count; i++) {
                char separator = '-';
                if(i == (Locations.Count - 1))
                    separator = '+';
                builder.AppendFormat("{0}{1}", Locations[i].Code, separator);
            }

            // Expiration time minutes are 15-aligned if
            // total expiration time is less than an hour.
            // Otherwise minutes are 30-aligned.
            int align = (Duration.Hours != 0) ? 30 : 15;
            int rem = Duration.Minutes % align;
            builder.AppendFormat("{0:D2}{1:D2}-", Duration.Hours, Duration.Minutes + ((rem != 0) ? (align - rem) : 0));

            builder.AppendFormat("{0:D3}{1:D2}{2:D2}-", TriggerTime.DayOfYear, TriggerTime.Hour, TriggerTime.Minute);
            builder.AppendFormat("{0}-", Callsign);

            return builder.ToString();
        }
    }
}
