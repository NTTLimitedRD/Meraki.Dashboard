using System;
using System.Collections.Generic;
using Meraki.Dashboard.Converters;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestUnixTimestampConverter
    {
        [Theory]
        [MemberData(nameof(ConvertTestData))]
        public void Convert(double timestamp, DateTime expectedDateTime)
        {
            Assert.Equal(expectedDateTime, UnixTimestampConverter.ToDateTime(timestamp));
            Assert.Equal(timestamp, UnixTimestampConverter.FromDateTime(UnixTimestampConverter.ToDateTime(timestamp)), 3);
        }

        public static IEnumerable<object[]> ConvertTestData()
        {
            yield return new object[] { "1474918068.66194", DateTime.Parse("2016-09-26T19:27:48.6620000") };
            yield return new object[] { "0.0", UnixTimestampConverter.EpochStart };
            yield return new object[] { "-100.0", UnixTimestampConverter.EpochStart.AddSeconds(-100.0) };
        }
    }
}
