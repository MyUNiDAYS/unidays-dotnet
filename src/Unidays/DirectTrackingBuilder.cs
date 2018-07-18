namespace Unidays
{
	public class DirectTrackingDetailsBuilder
    {
	    private DirectTrackingDetails _directTrackingDetails;
	    public DirectTrackingDetailsBuilder(string customerId, string currency, string transactionId )
	    {
		    _directTrackingDetails = new DirectTrackingDetails
		    {
				CustomerId = customerId,
				Currency = currency,
				TransactionId = transactionId
		    };
		}
		public DirectTrackingDetails Build()
	    {
		    return _directTrackingDetails;
	    }

		public DirectTrackingDetailsBuilder SetMemberId(string memberId)
	    {
		    _directTrackingDetails.MemberId = memberId;
		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetCode(string code)
	    {
		    _directTrackingDetails.Code = code;
		    return this;
	    }

		public DirectTrackingDetailsBuilder SetOrderTotal(decimal orderTotal)
	    {
		    _directTrackingDetails.OrderTotal = orderTotal;
		    return this;
	    }
	    public DirectTrackingDetailsBuilder SetItemsUNiDAYSDiscount(decimal itemsUnidaysDiscount)
	    {
		    _directTrackingDetails.ItemsUNiDAYSDiscount = itemsUnidaysDiscount;
		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetItemsTax(decimal itemsTax)
	    {
		    _directTrackingDetails.ItemsTax = itemsTax;
		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetShippingGross(decimal shippingGross)
	    {
		    _directTrackingDetails.ShippingGross = shippingGross;
		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetShippingDiscount(decimal shippingDiscount)
	    {
		    _directTrackingDetails.ShippingDiscount = shippingDiscount;

			return this;
		}
	    public DirectTrackingDetailsBuilder SetItemsGross(decimal itemsGross)
	    {
		    _directTrackingDetails.ItemsGross = itemsGross;

		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetItemsOtherDiscount(decimal itemsOtherDiscount)
	    {
		    _directTrackingDetails.ItemsOtherDiscount = itemsOtherDiscount;

		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetUNiDAYSDiscountPercentage(decimal discountPercentage)
	    {
		    _directTrackingDetails.UNiDAYSDiscountPercentage = discountPercentage;

		    return this;
	    }

	    public DirectTrackingDetailsBuilder SetNewCustomer(bool newCustomer)
	    {
		    _directTrackingDetails.NewCustomer = newCustomer;

		    return this;
	    }
	}

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
