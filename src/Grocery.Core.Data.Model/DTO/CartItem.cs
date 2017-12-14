namespace Grocery.Core.Data.Model.DTO
{
    using System;
    using System.ComponentModel;

    public class CartItem
    {
        [DisplayName("Product Id")]
        public int ProductId { get; set; }

        [DisplayName("Cart Id")]
        public int CartId { get; set; }

        [DisplayName("Addition Date")]
        public DateTime AddedOn { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }
    }
}
