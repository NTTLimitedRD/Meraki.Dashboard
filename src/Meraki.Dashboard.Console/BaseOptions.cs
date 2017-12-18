using System;
using CommandLine;

namespace Meraki.Dashboard.Console
{
    internal class BaseOptions
    {
        /// <summary>
        /// The environment variable to read the API key from if omitted from 
        /// the command line.
        /// </summary>
        /// <remarks>
        /// Change the HelpText in the Option attribute below when changing 
        /// this.
        /// </remarks>
        private const string ApiKeyEnvironmentVariable = "MERAKI_API_KEY";

        private string apiKey;

        [Option('k', "apiKey", HelpText = "Meraki API Key. Specify it here or by the 'MERAKI_API_KEY' environment variable.", MetaValue = "API_KEY", Required = false)]
        public string ApiKey
        {
            get
            {
                string result;
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    result = apiKey;
                }
                else
                {
                    result = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        throw new ArgumentException($"Meraki API Key not specified on the command line or in environment variable '{ApiKeyEnvironmentVariable}'");
                    }
                }
                return result;
            }
            set => apiKey = value;
        }
    }
}
