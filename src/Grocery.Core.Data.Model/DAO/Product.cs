namespace Grocery.Core.Data.Model.DAO
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime UpdatedOn { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CartItem> CartItem { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
