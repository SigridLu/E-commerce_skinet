using System;
namespace Core.Entities.OrderAggregate
{
	public class Order : BaseEntity
	{
        public Order() { }

        public Order(IReadOnlyList<DeliveryMethod> orderItems, string buyerEmail, Address shipToAddress,
			DeliveryMethod deliveryMethod, decimal subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;
		public Address ShipToAddress { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public IReadOnlyList<DeliveryMethod> OrderItems { get; set; }
		public decimal Subtotal { get; set; }
		public OrderStatus Status { get; set; } = OrderStatus.Pending;
		public string PaymentIntentId { get; set; }

        // When AutoMapper see this method prefix in Get, it will map the return value to the property named "Total".
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}

