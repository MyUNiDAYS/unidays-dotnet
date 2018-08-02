using System;
using Unidays.Client.Internal;

namespace Unidays.Client
{
    /// <summary>
    /// UNiDAYS DotNet Library for the Tracking API
    /// </summary>
    public sealed class TrackingHelper
    {
        private readonly DirectTrackingDetails _directTrackingDetails;

        public TrackingHelper(DirectTrackingDetails directTrackingDetails)
        {
            if (string.IsNullOrEmpty(directTrackingDetails.PartnerId))
                throw new ArgumentException("PartnerId is required", "partnerId");
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
        /// <returns>The URL to be placed inside a &lt;script /&gt; element in your receipt page. A JSON body will be returned detailing errors, if any</returns>
        public Uri TrackingScriptUrl()
        {
            return new UriGenerator(false).GenerateScriptUrl(_directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL
        /// </summary>
        /// <param name="key">The key for the signature</param>
        /// <returns>The URL to be placed inside a &lt;script /&gt; element in your receipt page. A JSON body will be returned detailing errors, if any</returns>
        public Uri TrackingScriptUrl(string key)
        {
            return new UriGenerator(false).GenerateScriptUrl(key, _directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL in Test Mode
        /// </summary>
        /// <returns>The URL to be placed inside a &lt;script /&gt; element in your receipt page. A JSON body will be returned detailing errors, if any</returns>
        public Uri TrackingScriptTestUrl()
        {
            return new UriGenerator(true).GenerateScriptUrl(_directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL in Test Mode
        /// </summary>
        /// <param name="key">The key for the signature</param>
        /// <returns>The URL to be placed inside a &lt;script /&gt; element in your receipt page. A JSON body will be returned detailing errors, if any</returns>
        public Uri TrackingScriptTestUrl(string key)
        {
            return new UriGenerator(true).GenerateScriptUrl(key, _directTrackingDetails);
        }
    }
}
