using EAS.Types.Enums;
using System;

namespace EAS.Types.Codes
{
    /// <summary>
    /// Location code indicates areas affected
    /// by the activated EAS alarm.
    /// </summary>
    public class LocationCode
    {
        public readonly string Code;

        public LocationCode(uint state, uint county, CountySubdivision subdivision = CountySubdivision.Unspecified)
        {
            Code = String.Format("{0:D1}{1:D2}{2:D3}", (uint)subdivision, state, county);
        }
    }
}
