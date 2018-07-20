using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Unidays.Internal;

namespace Unidays
{
    public sealed class TrackingClient
    {
        private readonly HttpClient _httpClient;
        private readonly DirectTrackingDetails _directTrackingDetails;
        private readonly string _key;

        public TrackingClient(HttpClient httpClient, DirectTrackingDetails directTrackingDetails, string key)
        {
            _httpClient = httpClient;
            _directTrackingDetails = directTrackingDetails;
            _key = key;
        }

        /// <summary>
        /// Sends a Server-to-Server Redemption Tracking Request
        /// </summary>
        /// <param name="sendTestParameter">Set to true to enable test mode</param>
        /// <returns>StatusCode of the resulting call</returns>
        public async Task<HttpStatusCode> SendAsync(bool sendTestParameter = false)
        {
            var uri = new UriGenerator(sendTestParameter).GenerateServerUrl(_key, _directTrackingDetails);
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri));

            return response.StatusCode;
        }
    }
}
