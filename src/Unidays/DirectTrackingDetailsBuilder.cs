namespace Unidays
{
    public class DirectTrackingDetailsBuilder
    {
        private DirectTrackingDetails _directTrackingDetails;
        public DirectTrackingDetailsBuilder(string customerId, string currency, string transactionId)
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

        public DirectTrackingDetailsBuilder WithMemberId(string memberId)
        {
            _directTrackingDetails.MemberId = memberId;
            return this;
        }

        public DirectTrackingDetailsBuilder WithCode(string code)
        {
            _directTrackingDetails.Code = code;
            return this;
        }

        public DirectTrackingDetailsBuilder WithOrderTotal(decimal orderTotal)
        {
            _directTrackingDetails.OrderTotal = orderTotal;
            return this;
        }
        public DirectTrackingDetailsBuilder WithItemsUNiDAYSDiscount(decimal itemsUnidaysDiscount)
        {
            _directTrackingDetails.ItemsUNiDAYSDiscount = itemsUnidaysDiscount;
            return this;
        }

        public DirectTrackingDetailsBuilder WithItemsTax(decimal itemsTax)
        {
            _directTrackingDetails.ItemsTax = itemsTax;
            return this;
        }

        public DirectTrackingDetailsBuilder WithShippingGross(decimal shippingGross)
        {
            _directTrackingDetails.ShippingGross = shippingGross;
            return this;
        }

        public DirectTrackingDetailsBuilder WithShippingDiscount(decimal shippingDiscount)
        {
            _directTrackingDetails.ShippingDiscount = shippingDiscount;

            return this;
        }
        public DirectTrackingDetailsBuilder WithItemsGross(decimal itemsGross)
        {
            _directTrackingDetails.ItemsGross = itemsGross;

            return this;
        }

        public DirectTrackingDetailsBuilder WithItemsOtherDiscount(decimal itemsOtherDiscount)
        {
            _directTrackingDetails.ItemsOtherDiscount = itemsOtherDiscount;

            return this;
        }

        public DirectTrackingDetailsBuilder WithUNiDAYSDiscountPercentage(decimal discountPercentage)
        {
            _directTrackingDetails.UNiDAYSDiscountPercentage = discountPercentage;

            return this;
        }

        public DirectTrackingDetailsBuilder WithNewCustomer(bool newCustomer)
        {
            _directTrackingDetails.NewCustomer = newCustomer;

            return this;
        }
    }
}
