using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace Meraki.Dashboard.Test
{
    /// <summary>
    /// Tests for <see cref="MerakiDashboardClient"/>.
    /// </summary>
    public class TestMerakiDashboardClient
    {
        [Fact]
        public void Ctor_MerakiDashboardClientSettings()
        {
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = baseAddress,
                ApiKey = apiKey
            };

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettings))
            { 
                Assert.Equal(baseAddress, merakiDashboardClient.Client.BaseAddress);
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
            }
        }

        [Fact]
        public void Ctor_IOptions()
        {
            Uri baseAddress = new Uri("http://www.myserver.com");
            const string apiKey = "000111222333444555666777888999000aaabbbbcccdddeee";

            MerakiDashboardClientSettingsOptions merakiDashboardClientSettingsOptions = new MerakiDashboardClientSettingsOptions
            {
                Value = new MerakiDashboardClientSettings
                {
                    BaseAddress = baseAddress,
                    ApiKey = apiKey
                }
            };

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(merakiDashboardClientSettingsOptions))
            {
                Assert.Equal(baseAddress, merakiDashboardClient.Client.BaseAddress);
                Assert.Equal(apiKey, merakiDashboardClient.Client.ApiKey);
            }
        }

        [Fact]
        public void Ctor_Null_MerakiDashboardClientSettings()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient((MerakiDashboardClientSettings) null));
        }

        [Fact]
        public void Ctor_MerakiDashboardClientSettings_Null_BaseAddress()
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = null,
                ApiKey = "apiKey"
            };

            Assert.Throws<ArgumentNullException>("baseAddress", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Ctor_MerakiDashboardClientSettings_Null_ApiKey(string apiKey)
        {
            MerakiDashboardClientSettings merakiDashboardClientSettings = new MerakiDashboardClientSettings
            {
                BaseAddress = new Uri("http://www.myserver.com"),
                ApiKey = apiKey
            };

            Assert.Throws<ArgumentException>("apiKey", () => new MerakiDashboardClient(merakiDashboardClientSettings));
        }

        [Fact]
        public void Ctor_Null_IOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient((IOptions<MerakiDashboardClientSettings>)null));
        }

        [Fact]
        public void Ctor_IOptions_Null_Settings()
        {
            Assert.Throws<ArgumentNullException>(() => new MerakiDashboardClient(new MerakiDashboardClientSettingsOptions()));
        }

        [Fact]
        public async void GetOrganizationAsync()
        {
            const string organizationId = "myOrg";
            Organization expectedOrganization = new Organization
            {
                Name = "organization name",
                Id = organizationId
            };

            Mock<MerakiHttpApiClient> apiClientMock = new Mock<MerakiHttpApiClient>(MockBehavior.Strict, "apiKey");
            apiClientMock.Setup(apiClient => apiClient.GetAsync<Organization>($"v0/organizations/{organizationId}"))
                         .Returns(Task.FromResult(expectedOrganization));
            // apiClientMock.As<IDisposable>().Setup(apiClient => apiClient.Dispose());
            apiClientMock.Protected().Setup("Dispose", true);

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(apiClientMock.Object))
            {
                Organization actualOrganization = await merakiDashboardClient.GetOrganizationAsync(organizationId);
                Assert.Equal(expectedOrganization, actualOrganization);
            }

            apiClientMock.VerifyAll();
        }

        [Fact]
        public async void GetOrganizationSnmpSettingsAsync()
        {
            const string organizationId = "myOrg";
            SnmpGetSettings snmpGetSettings = new SnmpGetSettings
            {
                V2cEnabled = false,
                V3Enabled = true,
                V3AuthenticationMode = SnmpAuthenticationMode.Sha,
                V3PrivacyMode = SnmpPrivacyMode.Aes128,
                Hostname = "snmp.meraki.com",
                Port = 162
            };

            Mock<MerakiHttpApiClient> apiClientMock = new Mock<MerakiHttpApiClient>(MockBehavior.Strict, "apiKey");
            apiClientMock.Setup(apiClient => apiClient.GetAsync<SnmpGetSettings>($"v0/organizations/{organizationId}/snmp"))
                                                      .Returns(Task.FromResult(snmpGetSettings));
            // apiClientMock.As<IDisposable>().Setup(apiClient => apiClient.Dispose());
            apiClientMock.Protected().Setup("Dispose", true);

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(apiClientMock.Object))
            {
                SnmpGetSettings actualSnmpGetSettings = await merakiDashboardClient.GetOrganizationSnmpSettingsAsync(organizationId);
                Assert.Equal(snmpGetSettings, actualSnmpGetSettings);
            }

            apiClientMock.VerifyAll();
        }

        [Fact]
        public async void PutOrganizationSnmpSettingsAsync()
        {
            const string organizationId = "myOrg";
            SnmpPutSettings snmpPutSettings = new SnmpPutSettings
            {
                V2cEnabled = false,
                V3Enabled = true,
                V3AuthenticationMode = SnmpAuthenticationMode.Sha,
                V3AuthenticationPassword = "auth password",
                V3PrivacyMode = SnmpPrivacyMode.Aes128,
                V3PrivacyPassword = "privacy password",
                PeerIps = new [] { IPAddress.Parse("192.168.0.1"), IPAddress.Parse("10.1.1.1") }
            };

            Mock<MerakiHttpApiClient> apiClientMock = new Mock<MerakiHttpApiClient>(MockBehavior.Strict, "apiKey");
            HttpStatusCode statusCode = HttpStatusCode.OK;
            apiClientMock.Setup(apiClient =>
                    apiClient.SendAsync(HttpMethod.Put, $"v0/organizations/{organizationId}/snmp", snmpPutSettings))
                .Returns(Task.FromResult(statusCode));
            // apiClientMock.As<IDisposable>().Setup(apiClient => apiClient.Dispose());
            apiClientMock.Protected().Setup("Dispose", true);

            using (MerakiDashboardClient merakiDashboardClient = new MerakiDashboardClient(apiClientMock.Object))
            {
                HttpStatusCode actualResponse =
                    await merakiDashboardClient.PutOrganizationSnmpSettingsAsync(organizationId, snmpPutSettings);
                Assert.Equal(statusCode, actualResponse);
            }

            apiClientMock.VerifyAll();
        }
    }
}
