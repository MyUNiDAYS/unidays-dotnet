using System;
using Unidays.Internal;

namespace Unidays
{
    /// <summary>
    /// UNiDAYS DotNet Library for the Tracking API
    /// </summary>
    public sealed class TrackingHelper
    {
        private readonly DirectTrackingDetails _directTrackingDetails;

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
        /// <param name="key">The key for the signature</param>
        /// <returns>The URL to make a server-to-server request to</returns>
        public Uri TrackingServerUrl(string key)
        {
            return new UriGenerator(false).GenerateServerUrl(key, _directTrackingDetails);
        }

        /// <summary>
        /// Generates the Server-to-Server Redemption Tracking URL in Test Mode
        /// </summary>
        /// <param name="key">The key for the signature</param>
        /// <returns>The test URL to make a server-to-server request to</returns>
        public Uri TrackingServerTestUrl(string key)
        {
            return new UriGenerator(true).GenerateServerUrl(key, _directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL
        /// </summary>
        /// <returns>The URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif</returns>
        public Uri TrackingPixelUrl()
        {
            return new UriGenerator(false).GeneratePixelUrl(_directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL
        /// </summary>
        /// <param name="key">The key for the signature</param>
        /// <returns>The URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif</returns>
        public Uri TrackingPixelUrl(string key)
        {
            return new UriGenerator(false).GeneratePixelUrl(key, _directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL in Test Mode
        /// </summary>
        /// <returns>The Test URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif</returns>
        public Uri TrackingPixelTestUrl()
        {
            return new UriGenerator(true).GeneratePixelUrl(_directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL in Test Mode
        /// </summary>
        /// <param name="key">The key for the signature</param>
        /// <returns>The test URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif</returns>
        public Uri TrackingPixelTestUrl(string key)
        {
            return new UriGenerator(true).GeneratePixelUrl(key, _directTrackingDetails);
        }
    }
}
