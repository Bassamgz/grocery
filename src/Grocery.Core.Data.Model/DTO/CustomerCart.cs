namespace Grocery.Core.Data.Model.DTO
{
    using System.ComponentModel;

    public class CustomerCart
    {
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("Cart Id")]
        public int CartId { get; set; }
    }
}
