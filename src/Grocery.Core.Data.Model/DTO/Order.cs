namespace Grocery.Core.Data.Model.DTO
{
    using System;
    using System.ComponentModel;

    public class Order
    {
        [DisplayName("Order Id")]
        public string Id { get; set; }

        [DisplayName("Purchased Date")]
        public DateTime PurchasedOn { get; set; }

        [DisplayName("Total Cost")]
        public decimal TotalCost { get; set; }
    }
}
