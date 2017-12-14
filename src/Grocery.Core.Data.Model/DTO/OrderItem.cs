namespace Grocery.Core.Data.Model.DTO
{
    using System.ComponentModel;

    public class OrderItem
    {
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [DisplayName("Order Id")]
        public string OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }
    }
}
