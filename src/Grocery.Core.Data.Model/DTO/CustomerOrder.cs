namespace Grocery.Core.Data.Model.DTO
{
    using System.ComponentModel;

    public class CustomerOrder
    {
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("Order Id")]
        public string OrderId { get; set; }
    }
}
