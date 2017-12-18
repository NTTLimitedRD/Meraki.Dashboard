# Contributing

As this library incomplete, pull requests are welcome. Please follow general C# programming guidelines and add
Xunit tests were possible.

In general:
1. The `MerakiDashboardClient` class is the main point of interaction and exposes one public method wrapping each of the Meraki Dashboard APIs.
1. Each API method should be in the form of `<Verb><MethodName>` where `<Verb>` is the HTTP verb (e.g. "Get", "Put") and `<MethodName>` is an indicative name.
1. Each API method should use the `MerakiHttpApiClient` class in the `Client` property to make the API calls. This provides authentication and a single point of mocking if needed. Add methods to `MerakiHttpApiClient` class for unsupported types of calls if needed.
1. Each API method should have one corresponding mocked test in the `TestMerakiDashboardClient` class in the `Meraki.Dashboard.Test` assembly. The aim is to quickly identify future breaking changes.
1. Each API method should escape any URIs passed to methods on the `Client` property using the protected `InterpolateAndEscape` method.
1. For new contracts, consider providing strong typing (e.g. `enum`s, `DateTime`s, arrays) for Meraki's weakly typed fields. Provide a field with a "Raw" suffix that accepts or provides the Meraki Dashboard API value and a more strongly typed version. Contracts should be in the `Meraki.Dashboard` namespace to prevent the need for users to include multiple namespaces.
1. For converting Meraki API weak types to stronger types, Create a class with a `Converter` suffix to convert to and from the type. The converter class should be in the `Meraki.Dashboard.Converters` namespace and should have corresponding tests in the `Meraki.Dashboard.Test` assembly. Meraki tends to use similar conversions so a suitable converter may already exist.
1. Update the list of supported APIs in README.md for newly wrapped APIs.
1. Ensure any personal Meraki API keys are not commited, including as debug build arguments.
1. While unlikely, use extension methods to extent `MerakiDashboardClientFactory`.

To assist in debugging API calls, instantiate a `MerakiHttpApiDebugContext` around calls to `MerakiHttpApiClient` 
methods. They will log details of data sent and received to Debug listeners, including the Debug window in 
Visual Studio. For example:

``` C#
using (new MerakiHttpApiDebugContext())
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	// Details are written to the debug window for the following call
	Network network = await merakiDashboardClient.GetNetworkAsync("v0/network/{id}/alerts");
}
```