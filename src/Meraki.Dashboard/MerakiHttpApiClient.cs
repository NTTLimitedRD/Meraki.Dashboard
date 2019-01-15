using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meraki.Dashboard
{
    /// <summary>
    /// Call HTTP APIs. Can be used to call Meraki APIs directly if not wrapped by <see cref="MerakiDashboardClient"/>.
    /// </summary>
    /// <remarks>
    /// Public (instead of internal) for testnig and mocking.
    /// </remarks>
    public class MerakiHttpApiClient : IDisposable
    {
        private const string AcceptTypeHttpHeader = "Accept-Type";
        private const string MerakiApiKeyHttpHeader = "X-Cisco-Meraki-API-Key";

        /// <summary>
        /// Create a new <see cref="MerakiHttpApiClient"/>.
        /// </summary>
        /// <param name="apiKey">
        /// The meraki API key, usually found in the user's profile in the Meraki Dashboard.
        /// This cannot be null, empty or whitespace.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="apiKey"/> cannot be null, empty or whitespace.
        /// </exception>
        public MerakiHttpApiClient(string apiKey)
            : this(apiKey, MerakiDashboardClientOptions.DefaultMerakiDashboardApiBaseAddress)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="MerakiHttpApiClient"/>.
        /// </summary>
        /// <param name="apiKey">
        /// The meraki API key, usually found in the user's profile in the Meraki Dashboard.
        /// This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseAddress">
        /// The optional base URI for web service calls if it differs from the Meraki defalt.
        /// If provided, this must be an absolute URI.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="baseAddress"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="apiKey"/> cannot be null, empty or whitespace. <paramref name="baseAddress"/>
        /// must be an absolute URI.
        /// </exception>
        public MerakiHttpApiClient(string apiKey, Uri baseAddress)
        {
            if (baseAddress == null)
            {
                throw new ArgumentNullException(nameof(baseAddress));
            }
            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException("Must be absolute URI", nameof(baseAddress));
            }
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(apiKey));
            }

            HttpClient = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = baseAddress
            };
            HttpClient.DefaultRequestHeaders.Add(MerakiApiKeyHttpHeader, apiKey);
            HttpClient.DefaultRequestHeaders.Add(AcceptTypeHttpHeader, "application/json");
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~MerakiHttpApiClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Actually clean up.
        /// </summary>
        /// <param name="calledFromDispose">
        /// true if called from Dispose, false if called from the finalizer.
        /// </param>
        protected virtual void Dispose(bool calledFromDispose)
        {
            if (calledFromDispose)
            {
                HttpClient?.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Base address used for calls.
        /// </summary>
        public Uri BaseAddress => HttpClient.BaseAddress;

        /// <summary>
        /// The Meraki Dashboard API key.
        /// </summary>
        public string ApiKey => HttpClient.DefaultRequestHeaders.GetValues(MerakiApiKeyHttpHeader).FirstOrDefault();

        /// <summary>
        /// Actually does the calls.
        /// </summary>
        internal HttpClient HttpClient { get; }

        /// <summary>
        /// Call the given URL asynchronously.
        /// </summary>
        /// <param name="method">
        /// </param>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <param name="content">
        /// The optional content to send.
        /// </param>
        /// <returns>
        /// The <see cref="HttpStatusCode"/> representing the result.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public virtual async Task<HttpStatusCode> SendAsync(HttpMethod method, string uri, string content = "")
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                if (MerakiHttpApiDebugContext.IsSet())
                {
                    Debug.WriteLine($"{method} {uri} sending: {content}");
                }

                request.Content = new StringContent(content);
                using (HttpResponseMessage response = await HttpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return response.StatusCode;
                }
            }
        }

        /// <summary>
        /// Call the given URL asynchronously.
        /// </summary>
        /// <param name="method">
        /// </param>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <param name="content">
        /// The optional content to send.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>. The caller is responsible for Disposing
        /// the returned object.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public virtual async Task<HttpStatusCode> SendAsync<T>(HttpMethod method, string uri, T content)
        {
            return await SendAsync(method, uri, JsonConvert.SerializeObject(content, JsonSerializerSettings));
        }

        /// <summary>
        /// Call the given URL asynchronously using an HTTP GET method, expecting returned data.
        /// </summary>
        /// <typeparam name="T">
        /// The expected return type.
        /// </typeparam>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The result as a string.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public virtual async Task<T> GetAsync<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await GetAsync(uri), JsonSerializerSettings);
        }

        /// <summary>
        /// Call the given URL asynchronously using an HTTP GET method, expecting returned data.
        /// </summary>
        /// <param name="uri">
        /// The URI to call.
        /// </param>
        /// <returns>
        /// The result as a string.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The request failed.
        /// </exception>
        public virtual async Task<string> GetAsync(string uri)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
            using (HttpResponseMessage response = await HttpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                if (MerakiHttpApiDebugContext.IsSet())
                {
                    Debug.WriteLine($"GET {uri} returned: {result}");
                }

                return result;
            }
        }

        private JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                JsonSerializerSettings result = new JsonSerializerSettings();
                if (MerakiHttpApiDebugContext.IsSet())
                {
                    result.Formatting = Formatting.Indented;
                }
                return result;
            }
        }
    }
}