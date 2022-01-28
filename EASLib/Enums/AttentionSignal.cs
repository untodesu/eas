namespace EASLib.Enums
{
    /// <summary>
    /// Attention signal is a signal that comes after
    /// the three S.A.M.E. data bursts and there're two
    /// separate signal types used for different reasons.
    /// </summary>
    public enum AttentionSignal
    {
        /// <summary>
        /// A 1050 Hz tone used by a Canadian weather
        /// radio network called Weatheradio.
        /// </summary>
        Weatheradio,

        /// <summary>
        /// A combined 853 Hz and 960 Hz tone used for
        /// radios and television in the US and probably Canada.
        /// </summary>
        RadioAndTV
    }
}
