namespace Grocery.Core.Data.Model.DAO
{
    using System;
    using System.Collections.Generic;

    public class Cart
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalCost { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CartItem> CartItem { get; set; }

        public virtual ICollection<CustomerCart> CustomerCart { get; set; }
    }
}
