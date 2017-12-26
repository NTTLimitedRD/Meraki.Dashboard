[![Build status](https://ci.appveyor.com/api/projects/status/oehy76art96ex6sp?svg=true)](https://ci.appveyor.com/project/anthonylangsworth/meraki-dashboard-r15c8)
[![NuGet Badge](https://buildstats.info/nuget/Meraki.Dashboard)](https://www.nuget.org/packages/Meraki.Dashboard/)

# Meraki Dashboard API .Net Standard Client

[Meraki](http://meraki.cisco.com) is a network appliance vendor acquired by Cisco in 2012 that focuses on 
cloud-hosted management and ease of use. Their devices are increasingly popular with companies of any size looking
to simply their network management.

Meraki provides a limited web API for managing their devices called the [Dashboard API](https://dashboard.meraki.com/api_docs). 
Meraki provides wrappers in NodeJS (JavaScript), Ruby and Python but not for C#. This library is the 
beginnings of a .Net Standard 2.0 wrapper. Its goals are:
1. Wrap the Meraki Dashboard APIs, making them callable with the least effort.
1. Follow C# standards and conventions as much as possible. For example, provide lists and enums with transparent conversions.
1. Allow easy mocking to make automated testing of calling code easier.
1. Allow APIs not wrapped by this library to be callable.
1. Provide assistance for debugging both this library and Meraki Dashboard API calls.

It is currently a work in progress. While this library is in beta, breaking changes are still possible.

See [LICENSE](LICENSE) for the license. Based off original work by Michael Park at https://github.com/migrap/Meraki. Meraki and Cisco are trademarks owned by Cisco Meraki and Cisco respectively.

## Use

Create a `MerakiDashboardClient` instance using `MerakiDashboardClientFactory.Create`, supplying your API key, 
then call methods on it, such as:

``` C#
using Meraki.Dashboard;

 // ...

string apiKey; // API key from the user's profile in the Meraki Dashboard
string organizationId; // my organization ID
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	try
	{
		// GET /organizations/[id] as documented at 
		// https://dashboard.meraki.com/api_docs#return-an-organization
		Organization organization = await merakiDashboardClient.GetOrganizationAsync(organizationId);

		// ...
	}
	catch(HttpRequestException ex)
	{
		// Hande errors
	}
}
```

`MerakiDashboardClientFactory.Create` also accepts an `IOption<MerakiClientOptions>` overload when 
loading settings from an appSettings.json file as shown in [TestOption.cs](test/Meraki.Dashboard.Test/TestOptions.cs).

If Meraki introduces a new Dashboard API where there is no support in the library, use the `Client` 
property to get access to the underlying `MerakiHttpApiClient` object that a `MerakiDashboardClient`
object uses to call Meraki Dashboard APIs.

For example, if Meraki introduced a `GET /networks/[id]/alerts` API to retreive the alerts
for each network (found on the Dashboard under "Network-wide" -> "Configure" -> "Alerts):

``` C#
using Meraki.Dashboard;

 // ...

 [DataContract]
 public class NetworkAlerts
 {
	[DataMember(Name="networkId")]
	public string NetworkId { get; set; }

	// Insert other fields
 }

string apiKey; // API key from the user's profile in the Meraki Dashboard
string networkId; // my network ID
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	NetworkAlerts networkAlerts = 
		await merakiDashboardClient.Client.GetAsync<NetworkAlerts>("v0/network/{id}/alerts");

	// ...
}
```

The [Meraki Dashboard APIs](https://dashboard.meraki.com/api_docs) with at least partial support are:

- [x] GET /organizations/[organization_id]/admins
- [ ] POST /organizations/[organization_id]/admins
- [ ] PUT /organizations/[organization_id]/admins/[id]
- [ ] DELETE /organizations/[organization_id]/admins/[id]
- [x] GET /devices/[serial]/clients 
- [ ] GET /networks/[networkId]/clients/[client_mac]/policy
- [ ] PUT /networks/[id]/clients/[mac]/policy
- [ ] GET /networks/[id]/clients/[mac]/splashAuthorizationStatus
- [ ] PUT /networks/[id]/clients/[mac]/splashAuthorizationStatus
- [ ] GET /organizations/[organizationId]/configTemplates
- [ ] DELETE /organizations/[organizationId]/configTemplates/[id]
- [x] GET /networks/[networkId]/devices
- [x] GET /networks/[networkId]/devices/[serial]
- [ ] GET /networks/[networkId]/devices/[serial]/uplink 
- [ ] PUT /networks/[networkId]/devices/[serial]
- [ ] POST /networks/[networkId]/devices/claim
- [ ] GET /networks/[networkId]/devices/[serial]/lldp_cdp
- [ ] PUT /networks/[networkId]/cellularFirewallRules
- [ ] GET /networks/[networkId]/l3FirewallRules
- [ ] PUT /networks/[networkId]/l3FirewallRules
- [ ] GET /organizations/[organizationId]/vpnFirewallRules
- [ ] PUT /organizations/[organizationId]/vpnFirewallRules
- [ ] GET /networks/[networkId]/ssids/[number]/l3FirewallRules
- [ ] PUT /networks/[networkId]/ssids/[number]/l3FirewallRules
- [ ] GET /networks/[id]/groupPolicies
- [x] GET /organizations/[organizationId]/networks 
- [x] GET /networks/[id]
- [ ] PUT /networks/[id]
- [ ] POST /organizations/[organizationId]/networks
- [ ] DELETE /networks/[id]
- [ ] POST /networks/[id]/bind
- [ ] POST /networks/[id]/unbind
- [ ] GET /networks/[id]/siteToSiteVpn
- [ ] PUT /networks/[id]/siteToSiteVpn
- [x] GET /networks/[id]/traffic
- [ ] GET /networks/[id]/accessPolicies
- [ ] GET /networks/[id]/airMarshal
- [ ] GET /networks/[id]/bluetoothSettings
- [ ] PUT /networks/[id]/bluetoothSettings
- [x] GET /organizations
- [x] GET /organizations/[id]
- [ ] PUT /organizations/[id]
- [ ] POST /organizations
- [ ] POST /organizations/[id]/clone
- [ ] POST /organizations/[id]/claim
- [x] GET /organizations/[id]/licenseState
- [x] GET /organizations/[id]/inventory
- [x] GET /organizations/[id]/snmp
- [x] PUT /organizations/[id]/snmp
- [ ] GET /organizations/[id]/thirdPartyVPNPeers
- [ ] PUT /organizations/[id]/thirdPartyVPNPeers
- [ ] PUT /organizations/[id]/thirdPartyVPNPeers
- [ ] GET /networks/[networkId]/phoneAssignments
- [ ] GET /networks/[networkId]/phoneAssignments/[serial]
- [ ] PUT /networks/[networkId]/phoneAssignments/[serial]
- [ ] DELETE /networks/[networkId]/phoneAssignments/[serial]
- [ ] GET /networks/[networkId]/phoneContacts
- [ ] POST /networks/[networkId]/phoneContacts
- [ ] PUT /networks/[networkId]/phoneContacts/[contactId]
- [ ] DELETE /networks/[networkId]/phoneContacts/[contactId]
- [ ] GET /networks/[networkId]/phoneNumbers
- [ ] GET /networks/[networkId]/phoneNumbers/available
- [ ] GET /organizations/[organizationId]/samlRoles
- [ ] GET /organizations/[organizationId]/samlRoles/[id]
- [ ] PUT /organizations/[organizationId]/samlRoles/[id]
- [ ] POST /organizations/[organizationId]/samlRoles
- [ ] DELETE /organizations/[organizationId]/samlRoles/[id]
- [ ] GET /networks/[network_id]/sm/devices
- [ ] PUT /networks/[network_id]/sm/devices/tags
- [ ] PUT /networks/[network_id]/sm/device/fields
- [ ] PUT /networks/[network_id]/sm/devices/lock
- [ ] PUT /networks/[network_id]/sm/device/wipe
- [ ] PUT /networks/[network_id]/sm/devices/checkin
- [ ] PUT /networks/[network_id]/sm/devices/move
- [ ] GET /networks/[networkId]/ssids
- [ ] GET /networks/[networkId]/ssids/[number]
- [ ] PUT /networks/[networkId]/ssids/[number]
- [x] GET /devices/[serial]/switchPorts
- [ ] GET /devices/[serial]/switchPorts/[number]
- [ ] PUT /devices/[serial]/switchPorts/[number]
- [ ] GET /networks/[networkId]/staticRoutes
- [ ] GET /networks/[networkId]/staticRoutes/[srId]
- [ ] PUT /networks/[networkId]/staticRoutes/[srId]
- [ ] POST /networks/[networkId]/staticRoutes
- [ ] DELETE /networks/[networkId]/staticRoutes/[srId] 
- [x] GET /networks/[networkId]/vlans
- [ ] GET /networks/[networkId]/vlans/[vlanId]
- [ ] PUT /networks/[networkId]/vlans/[vlanId]
- [ ] POST /networks/[networkId]/vlans
- [ ] DELETE /networks/[networkId]/vlans/[id]

## Mocking

As stated above, one of the main goals for providing a client library is 
mocking it when unit testing the calling code as shown in 
[TestMerakiDashboardClientMocking.cs](test/Meraki.Dashboard.Test/TestMerakiDashboardClientMocking.cs).

## Exercise

The exercises found at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab can be found 
in the `Meraki.Dashboard.Console` project. At the time of writing, the Meraki organization and devices shown does
not match the documented exercises. However, this may still be helpful to those looking for more complex
examples.

## Contributing

Pull requests are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for contribution details.
