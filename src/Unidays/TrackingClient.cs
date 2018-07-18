using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Unidays
{
	class TrackingClient
    {
	    const string trackingUrl = "https://tracking.myunidays.com/perks/redemption/v1.1";

		private readonly HttpClient _httpClient;
	    private readonly string _customerId;
	    private readonly byte[] _key;

	    public TrackingClient(HttpClient httpClient, string customerId, byte[] key)
	    {
		    _httpClient = httpClient;
		    _customerId = customerId;
		    _key = key;
	    }

	    public async Task<HttpStatusCode> PostAsync(DirectTrackingDetails directTrackingDetails)
	    {
			var uri = new UriGenerator().GenerateSignedUrl(_key, directTrackingDetails);

		    var response = await _httpClient.PostAsync(uri, null, CancellationToken.None);
			
			return response.StatusCode;
	    }
	}
}
