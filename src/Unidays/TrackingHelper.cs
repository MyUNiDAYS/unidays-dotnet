using System;

namespace Unidays
{
    /// <summary>
    /// UNiDAYS DotNet Library for the Tracking API
    /// </summary>
    public sealed class TrackingHelper
    {
        readonly string customerId;
        readonly byte[] key;
        const string trackingUrl = "https://tracking.myunidays.com/perks/redemption/v1.1";

        public TrackingHelper(string customerId, byte[] key)
        {
            if (string.IsNullOrEmpty(customerId))
                throw new ArgumentException("CustomerId is required", "customerId");

            if (key == null)
                throw new ArgumentNullException("key", "Key cannot be null");

            if (key.Length == 0)
                throw new ArgumentException("Key cannot be empty", "key");


            this.customerId = customerId;
            this.key = key;
        }

        /// <summary>
        /// Generates the Server-to-Server Redemption Tracking URL
        /// </summary>
        /// <param name="transactionId">Unique ID for the transaction</param>
        /// <param name="memberId"></param>
        /// <param name="currency">ISO 4217 Currency Code</param>
        /// <param name="orderTotal">Order total rounded to 2 d.p.</param>
        /// <param name="itemsUNiDAYSDiscount"></param>
        /// <param name="code"></param>
        /// <param name="itemsTax"></param>
        /// <param name="shippingGross"></param>
        /// <param name="shippingDiscount">Shipping discount to 2 d.p.</param>
        /// <param name="itemsGross"></param>
        /// <param name="itemsOtherDiscount"></param>
        /// <param name="UNiDAYSDiscountPercentage"></param>
        /// <param name="newCustomer">1 if the member is a new customer, 0 if not</param>
        /// <returns>The URL to make a server-to-server request to.</returns>
        public Uri SignedDirectTrackingUrl(DirectTrackingDetails directTrackingDetails)
        {
			return new UriGenerator().GenerateSignedUrl(this.key, directTrackingDetails);
        }

        /// <summary>
        /// Generates the Redemption Tracking URL
        /// </summary>
        /// <param name="transactionId">Unique ID for the transaction</param>
        /// <param name="memberId"></param>
        /// <param name="currency">ISO 4217 Currency Code</param>
        /// <param name="orderTotal">Order total rounded to 2 d.p.</param>
        /// <param name="itemsUNiDAYSDiscount"></param>
        /// <param name="code">Discount code used by the member</param>
        /// <param name="itemsTax"></param>
        /// <param name="shippingGross"></param>
        /// <param name="shippingDiscount">Shipping discount to 2 d.p.</param>
        /// <param name="itemsGross"></param>
        /// <param name="itemsOtherDiscount"></param>
        /// <param name="UNiDAYSDiscountPercentage"></param>
        /// <param name="newCustomer">1 if the member is a new customer, 0 if not</param>
        /// <returns>The URL to be placed inside an &lt;img /&gt; element in your receipt page. The image returned is a 1x1px transparent gif.</returns>
        public Uri ClientSideTrackingPixelUrl(DirectTrackingDetails directTrackingDetails)
        {
	        return new UriGenerator().GeneratePixelUrl(directTrackingDetails);
        }
    }
}