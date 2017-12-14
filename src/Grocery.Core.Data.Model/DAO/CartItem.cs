namespace Grocery.Core.Data.Model.DAO
{
    using System;

    public class CartItem
    {
        public int ProductId { get; set; }

        public int CartId { get; set; }

        public DateTime AddedOn { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
