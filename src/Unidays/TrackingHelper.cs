﻿/*
The MIT License (MIT)

Copyright (c) 2017 MyUNiDAYS Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

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

            GenerateQuery(builder, this.customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
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

            GenerateQuery(builder, this.customerId, transactionId, memberId, currency, orderTotal, itemsUNiDAYSDiscount, code, itemsTax,
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

        static void GenerateQuery(StringBuilder builder, string customerId, string transactionId, string memberId, string currency, decimal? orderTotal, decimal? itemsUNiDAYSDiscount, string code, decimal? itemsTax, decimal? shippingGross, decimal? shippingDiscount, decimal? itemsGross, decimal? itemsOtherDiscount, decimal? UNiDAYSDiscountPercentage, int? newCustomer)
        {
            builder
                .Append("?CustomerId=")
                .Append(HttpUtility.UrlEncode(customerId))
                .Append("&TransactionId=")
                .Append(HttpUtility.UrlEncode(transactionId))
                .Append("&MemberId=")
                .Append(HttpUtility.UrlEncode(memberId))
                .Append("&Currency=")
                .Append(HttpUtility.UrlEncode(currency))
                .Append("&OrderTotal=");

            if (orderTotal.HasValue)
                builder.AppendFormat("{0:0.00}", orderTotal.Value);

            builder.Append("&ItemsUNiDAYSDiscount=");
            if (itemsUNiDAYSDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", itemsUNiDAYSDiscount.Value);

            builder.Append("&Code=").Append(HttpUtility.UrlEncode(code));

            builder.Append("&ItemsTax=");
            if (itemsTax.HasValue)
                builder.AppendFormat("{0:0.00}", itemsTax.Value);

            builder.Append("&ShippingGross=");
            if (shippingGross.HasValue)
                builder.AppendFormat("{0:0.00}", shippingGross.Value);

            builder.Append("&ShippingDiscount=");
            if (shippingDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", shippingDiscount.Value);

            builder.Append("&ItemsGross=");
            if (itemsGross.HasValue)
                builder.AppendFormat("{0:0.00}", itemsGross.Value);

            builder.Append("&ItemsOtherDiscount=");
            if (itemsOtherDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", itemsOtherDiscount.Value);

            builder.Append("&UNiDAYSDiscountPercentage=");
            if (UNiDAYSDiscountPercentage.HasValue)
                builder.AppendFormat("{0:0.00}", UNiDAYSDiscountPercentage.Value);

            builder.Append("&NewCustomer=");
            if (newCustomer.HasValue)
                builder.AppendFormat("{0}", newCustomer);
        }
    }
}