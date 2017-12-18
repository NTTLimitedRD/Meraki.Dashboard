using System;
using MerakiDashboard;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Factory to create <see cref="MerakiDashboardClient"/> objects.
    /// </summary>
    /// <remarks>
    /// To extend, add extension methods. 
    /// </remarks>
    public static class MerakiDashboardClientFactory
    {
        /// <summary>
        /// Create a <see cref="MerakiDashboardClient"/> based off the given <see cref="MerakiDashboardClientSettings"/> object.
        /// </summary>
        /// <param name="configure">
        /// An optional configuration step on the <see cref="MerakiDashboardClientSettings"/> object being created.
        /// </param>
        /// <returns>
        /// A configured <see cref="MerakiDashboardClient"/> object.
        /// </returns>
        public static MerakiDashboardClient Create(Action<MerakiDashboardClientSettings> configure = null)
        {
            MerakiDashboardClientSettings settings = new MerakiDashboardClientSettings();
            MerakiDashboardClientSettingsSetup setup = new MerakiDashboardClientSettingsSetup();

            setup.Configure(settings);
            configure?.Invoke(settings);

            return new MerakiDashboardClient(settings);
        }

        /// <summary>
        /// Create a <see cref="MerakiDashboardClient"/> using the given <paramref name="apiKey"/>.
        /// </summary>
        /// <param name="apiKey">
        /// The API Key to use. This cannot be null, empty or whitespace.
        /// </param>
        /// <returns>
        /// A configured <see cref="MerakiDashboardClient"/> object.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="apiKey"/> cannot be null, empty or whitespace.
        /// </exception>
        public static MerakiDashboardClient Create(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));   
            }

            return Create(mcs => mcs.ApiKey = apiKey);
        }
    }
}