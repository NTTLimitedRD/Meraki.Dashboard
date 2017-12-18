using Xunit;

namespace Meraki.Dashboard.Test
{
    public class TestMerakiHttpDebugContext
    {
        [Fact]
        public void IsSet()
        {
            Assert.False(MerakiHttpApiDebugContext.IsSet());
            using (new MerakiHttpApiDebugContext())
            {
                Assert.True(MerakiHttpApiDebugContext.IsSet());
                using (new MerakiHttpApiDebugContext())
                {
                    Assert.True(MerakiHttpApiDebugContext.IsSet());
                }
                Assert.True(MerakiHttpApiDebugContext.IsSet());
            }
            Assert.False(MerakiHttpApiDebugContext.IsSet());
        }
    }
}
