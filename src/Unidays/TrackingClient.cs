using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Unidays.Internal;

namespace Unidays
{
    public sealed class TrackingClient
    {
        private readonly DirectTrackingDetails _directTrackingDetails;
        private readonly string _key;
        private readonly HttpClient _httpClient;

        public TrackingClient(DirectTrackingDetails directTrackingDetails, string key, HttpClient httpClient = null)
        {
            _directTrackingDetails = directTrackingDetails;
            _key = key;
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Sends a Server-to-Server Redemption Tracking Request
        /// </summary>
        /// <param name="sendTestParameter">Set to true to enable test mode</param>
        /// <returns>HttpResponseMessage of the resulting call</returns>
        public async Task<HttpResponseMessage> SendAsync(bool sendTestParameter = false)
        {
            var uri = new UriGenerator(sendTestParameter).GenerateServerUrl(_key, _directTrackingDetails);
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri));

            return response;
        }
    }
}
