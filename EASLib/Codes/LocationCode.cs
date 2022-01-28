using EASLib.Enums;
using System;

namespace EASLib.Codes
{
    /// <summary>
    /// Location codes determine the areas
    /// affected by the triggered EAS alarm.
    /// </summary>
    public class LocationCode
    {
        public readonly string Code;

        /// <summary>
        /// Constructs a LocationCode object for a US location. <br/>
        /// See https://en.wikipedia.org/wiki/Federal_Information_Processing_Standard_state_code <br/>
        /// </summary>
        /// <param name="state">State FIPS ID</param>
        /// <param name="county">County/whatever_subdivision FIPS ID</param>
        /// <param name="subdivision">General part of the County/whatever_subdivision</param>
        public LocationCode(uint state, uint county, USCountySubdivision subdivision = USCountySubdivision.Unspecified)
        {
            Code = String.Format("{0:D1}{1:D2}{2:D3}", (uint)subdivision, state, county);
        }

        /// <summary>
        /// Constructs a LocationCode object for either a manual US location
        /// or for the Canadian Weatheradio location code.<br/>
        /// See https://en.wikipedia.org/wiki/Federal_Information_Processing_Standard_state_code <br/>
        /// See https://www.canada.ca/en/environment-climate-change/services/weatheradio/specific-area-message-encoding/forecast-regions.html <br/>
        /// </summary>
        /// <remarks>Merasmus! Take your voodoo back to Canada! Where it belongs!"</remarks>
        /// <param name="code">Location code as a six-or-less-digit value</param>
        public LocationCode(uint code)
        {
            Code = String.Format("{0:D6}", code);
        }
    }
}
