using System.Collections.Generic;
using Meraki.Dashboard.Converters;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestSnmpAuthenticationModeConverter
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(string raw, SnmpAuthenticationMode expectedSnmpAuthenticationMode)
        {
            Assert.Equal(expectedSnmpAuthenticationMode, SnmpAuthenticationModeConverter.ToEnum(raw));
            Assert.Equal(raw, SnmpAuthenticationModeConverter.FromEnum(SnmpAuthenticationModeConverter.ToEnum(raw)));
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { null, SnmpAuthenticationMode.Unknown };
            yield return new object[] { "SHA", SnmpAuthenticationMode.Sha };
            yield return new object[] { "MD5", SnmpAuthenticationMode.Md5 };
        }
    }
}
