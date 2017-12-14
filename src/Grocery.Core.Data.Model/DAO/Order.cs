namespace Grocery.Core.Data.Model.DAO
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public string Id { get; set; }

        public DateTime PurchasedOn { get; set; }

        public decimal TotalCost { get; set; }

        // Navigations Lazy Loading
        public virtual ICollection<CustomerOrder> CustomerOrder { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
