using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Unidays
{
	public class TrackingClient
    {
	    const string trackingUrl = "https://tracking.myunidays.com/perks/redemption/v1.2";

		private readonly HttpClient _httpClient;
	    private readonly DirectTrackingDetails _directTrackingDetails;
	    private readonly byte[] _key;

	    public TrackingClient(HttpClient httpClient, DirectTrackingDetails directTrackingDetails, byte[] key)
	    {
		    _httpClient = httpClient;
			_directTrackingDetails = directTrackingDetails;
		    _key = key;
	    }

	    /// <summary>
	    /// Sends a Server-to-Server Redemption Tracking Request
	    /// </summary>
	    /// <returns>StatusCode of the resulting call</returns>
		public async Task<HttpStatusCode> SendAsync()
	    {
			var uri = new UriGenerator().GenerateServerUrl(_key, _directTrackingDetails);
		    var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri));
			
			return response.StatusCode;
	    }
	}
}
