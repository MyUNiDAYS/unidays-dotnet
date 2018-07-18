namespace Unidays
{
	public class DirectTrackingDetails
	{
		public string CustomerId { get; set; }
		public string TransactionId { get; set; }
		public string Currency { get; set; }

		public string MemberId { get; set; }
		public string Code { get; set; }
		public decimal? OrderTotal { get; set; }
		public decimal? ItemsUNiDAYSDiscount { get; set; }
		public decimal? ItemsTax { get; set; }
		public decimal? ShippingGross { get; set; }
		public decimal? ShippingDiscount { get; set; }
		public decimal? ItemsGross { get; set; }
		public decimal? ItemsOtherDiscount { get; set; }
		public decimal? UNiDAYSDiscountPercentage { get; set; }
		public bool? NewCustomer { get; set; }
		public string Signature { get; set; }
	}
}