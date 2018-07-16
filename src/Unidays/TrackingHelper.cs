namespace Unidays
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    /// <summary>
    /// UNiDAYS SDK Helper Class
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
        public string ServerToServerTrackingUrl(string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
        {
            var builder = new StringBuilder();

	        new UriBuilder().GenerateQueryString(builder, this.customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
                shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage, newCustomer);
            SignUrl(builder, "Signature", this.key);

            builder.Insert(0, trackingUrl);

            return builder.ToString();
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
        public string ClientSideTrackingPixelUrl(string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
        {
            var builder = new StringBuilder();
	        new UriBuilder().GenerateQueryString(builder, this.customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
                shippingGross, shippingDiscount, itemsGross, itemsOtherDiscount, UNiDAYSDiscountPercentage, newCustomer);
            SignUrl(builder, "Signature", this.key);

            builder.Insert(0, trackingUrl);
            builder.Insert(trackingUrl.Length, ".gif");

            return builder.ToString();
        }

        static void SignUrl(StringBuilder builder, string param, byte[] key)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = key;
                
                hmac.Initialize();
                var buffer = Encoding.ASCII.GetBytes(builder.ToString());
                var signatureBytes = hmac.ComputeHash(buffer);
                var signature = Convert.ToBase64String(signatureBytes);

                builder
                    .Append('&')
                    .Append(param)
                    .Append('=')
                    .Append(HttpUtility.UrlEncode(signature));
            }
        }
    }
}