using System;

namespace Unidays
{
    /// <summary>
    /// UNiDAYS DotNet Library for the Tracking API
    /// </summary>
    public sealed class TrackingHelper
    {
	    private DirectTrackingDetails _directTrackingDetails;

		public TrackingHelper(DirectTrackingDetails directTrackingDetails)
        {
            if (string.IsNullOrEmpty(directTrackingDetails.CustomerId))
                throw new ArgumentException("CustomerId is required", "customerId");
	        if (string.IsNullOrEmpty(directTrackingDetails.Currency))
		        throw new ArgumentException("Currency is required", "currency");
	        if (string.IsNullOrEmpty(directTrackingDetails.TransactionId))
		        throw new ArgumentException("TransactionId is required", "transactionId");

			_directTrackingDetails = directTrackingDetails;
        }

		/// <summary>
		/// Generates the Server-to-Server Redemption Tracking URL
		/// </summary>
		/// <param name="key">the key for the signature</param>
		/// <returns>The URL to make a server-to-server request to.</returns>
		public Uri DirectTrackingUrl(byte[] key)
        {
			return new UriGenerator().GenerateServerRequestUrl(key, _directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL
        /// </summary>
        /// <returns>The URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif.</returns>
        public Uri TrackingPixelUrl()
        {
	        return new UriGenerator().GeneratePixelUrl(_directTrackingDetails);
        }

	    /// <summary>
	    /// Generates the Redemption Tracking URL
	    /// </summary>
	    /// <returns>The URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif.</returns>
	    public Uri TrackingPixelUrl(byte[] key)
	    {
		    return new UriGenerator().GeneratePixelUrl(key, _directTrackingDetails);
	    }
	}
}