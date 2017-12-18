using System;
using System.Collections.Generic;
using Meraki.Dashboard.Converters;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestLicenceExpirationDateConverter
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(string licenceExpirationDate, DateTime expectedDateTime)
        {
            Assert.Equal(expectedDateTime, LicenceExpirationDateConverter.ToDateTime(licenceExpirationDate));
            Assert.Equal(licenceExpirationDate, 
                LicenceExpirationDateConverter.FromDateTime(LicenceExpirationDateConverter.ToDateTime(licenceExpirationDate)));
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { "Aug 28, 2021 UTC", new DateTime(2021, 08, 28, 0, 0, 0, DateTimeKind.Utc) };
        }
    }
}
