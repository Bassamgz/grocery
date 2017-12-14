namespace Grocery.Core.Data.Model.DTO
{
    using System;
    using System.ComponentModel;

    public class Cart
    {
        [DisplayName("Cart Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Total Cost")]
        public decimal TotalCost { get; set; }
    }
}
