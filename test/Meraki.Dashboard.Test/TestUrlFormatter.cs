using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestUrlFormatter
    {
        [Theory]
        [InlineData("", "abc", "abc")]
        [InlineData(null, 1, "1")]
        [InlineData(UrlFormatter.DoNotEscape, "abc", "abc")]
        [InlineData(UrlFormatter.DoNotEscape, 1, "1")]
        [InlineData("", "a/b", "a%2Fb")]
        [InlineData(UrlFormatter.DoNotEscape, "a/b", "a/b")]
        public void Format(string format, object arg, string expectedResult)
        {
            Assert.Equal(expectedResult, new UrlFormatter().Format(format, arg, null));
        }

        [Theory]
        [InlineData("X")]
        [InlineData("1234")]
        public void Format_InvalidFormat(string format)
        {
            Assert.Throws<FormatException>(() => new UrlFormatter().Format(format, "abc", null));
        }
    }
}
