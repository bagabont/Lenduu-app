using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.Util;
using Newtonsoft.Json;

namespace Humance.Push.AeroGear
{
    /// <summary>
    /// Represents an Http AeroGear client.
    /// </summary>
    public class AeroGearClient
    {
        private readonly IAeroGearConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="AeroGearClient"/> class.
        /// </summary>
        /// <param name="config">Platform dependend configuration.</param>
        public AeroGearClient(IAeroGearConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sends a registration request to the specified AeroGear unified push notifications server.
        /// </summary>
        /// <param name="aeroGearServer">AeroGear server URI.</param>
        public async Task RegisterAsync(Uri aeroGearServer)
        {
            var request = CreateHttpRequest(aeroGearServer, _config.VariantId, _config.VariantSecret);
            var requestStream = await request.GetRequestStreamAsync();
            using (var sw = new StreamWriter(requestStream))
            {
                // Build the JSON object which carries the device's registration information
                var json = JsonConvert.SerializeObject(_config);

                await sw.WriteAsync(json);
                await sw.FlushAsync();

                // If a request fails, it will throw a WebException
                using (var response = await request.GetResponseAsync())
                {
                }
            }
        }

        private HttpWebRequest CreateHttpRequest(Uri aerogearServer, string username, string password)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(aerogearServer);
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.PreAuthenticate = true;
            var cache = new CredentialCache();
            cache.Add(aerogearServer, "Basic", new NetworkCredential(username, password));
            request.Credentials = cache;
            return request;
        }
    }
}