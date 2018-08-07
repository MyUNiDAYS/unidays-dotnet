using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Unidays.Client.Internal
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendTrackingParameters(this StringBuilder builder, DirectTrackingDetails directTrackingDetails)
        {
            builder
                .Append("?PartnerId=")
                .Append(HttpUtility.UrlEncode(directTrackingDetails.PartnerId))
                .Append("&TransactionId=")
                .Append(HttpUtility.UrlEncode(directTrackingDetails.TransactionId))
                .Append("&Currency=")
                .Append(HttpUtility.UrlEncode(directTrackingDetails.Currency));
            
            builder.Append("&MemberId=");
            if (!string.IsNullOrEmpty(directTrackingDetails.MemberId))
               builder.AppendFormat(HttpUtility.UrlEncode(directTrackingDetails.MemberId));
            
            builder.Append("&Code=");
            if (!string.IsNullOrEmpty(directTrackingDetails.Code))
                builder.AppendFormat(HttpUtility.UrlEncode(directTrackingDetails.Code));
           
            builder.Append("&OrderTotal=");
            if (directTrackingDetails.OrderTotal.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.OrderTotal.Value);

            builder.Append("&ItemsUNiDAYSDiscount=");
            if (directTrackingDetails.ItemsUNiDAYSDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsUNiDAYSDiscount.Value);

            builder.Append("&ItemsTax=");
            if (directTrackingDetails.ItemsTax.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsTax.Value);
           
            builder.Append("&ShippingGross=");
            if (directTrackingDetails.ShippingGross.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ShippingGross.Value);

            builder.Append("&ShippingDiscount=");
            if (directTrackingDetails.ShippingDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ShippingDiscount.Value);

            builder.Append("&ItemsGross=");
            if (directTrackingDetails.ItemsGross.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsGross.Value);

            builder.Append("&ItemsOtherDiscount=");
            if (directTrackingDetails.ItemsOtherDiscount.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.ItemsOtherDiscount.Value);

            builder.Append("&UNiDAYSDiscountPercentage=");
            if (directTrackingDetails.UNiDAYSDiscountPercentage.HasValue)
                builder.AppendFormat("{0:0.00}", directTrackingDetails.UNiDAYSDiscountPercentage.Value);

            builder.Append("&NewCustomer=");
            if (directTrackingDetails.NewCustomer.HasValue)
                builder.AppendFormat("{0}", directTrackingDetails.NewCustomer);

            return builder;
        }

        public static StringBuilder AppendSignature(this StringBuilder builder, string key)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = Convert.FromBase64String(key);

                hmac.Initialize();
                var buffer = Encoding.ASCII.GetBytes(builder.ToString());
                var signatureBytes = hmac.ComputeHash(buffer);
                builder.Append($"&Signature={HttpUtility.UrlEncode(Convert.ToBase64String(signatureBytes))}");
                return builder;
            }
        }

        public static StringBuilder AppendTestParameter(this StringBuilder builder, bool isTestUri)
        {
            if (isTestUri)
                builder.Append("&Test=True");
            return builder;
        }
    }
}
