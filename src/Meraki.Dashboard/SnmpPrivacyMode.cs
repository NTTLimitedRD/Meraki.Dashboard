namespace Meraki.Dashboard
{
    /// <summary>
    /// SNMP privacy mode.
    /// </summary>
    public enum SnmpPrivacyMode
    {
        /// <summary>
        /// Unknown or no value set.
        /// </summary>
        Unknown,

        /// <summary>
        /// Data Encryption Standard (DES). Obsolete.
        /// </summary>
        Des,

        /// <summary>
        /// Advanced Encryption Standard with a 128-bit key. Recommended.
        /// </summary>
        Aes128
    }
}
