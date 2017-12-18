using System.Collections.Generic;
using Meraki.Dashboard.Converters;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestSnmpPrivacyModeConverter
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(string raw, SnmpPrivacyMode expectedSnmpPrivacyMode)
        {
            Assert.Equal(expectedSnmpPrivacyMode, SnmpPrivacyModeConverter.ToEnum(raw));
            Assert.Equal(raw, SnmpPrivacyModeConverter.FromEnum(SnmpPrivacyModeConverter.ToEnum(raw)));
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { null, SnmpPrivacyMode.Unknown };
            yield return new object[] { "DES", SnmpPrivacyMode.Des };
            yield return new object[] { "AES128", SnmpPrivacyMode.Aes128 };
        }
    }
}
