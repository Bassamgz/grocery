namespace Grocery.Core.Data.Model.DTO
{
    using System;
    using System.ComponentModel;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Updated On")]
        public DateTime UpdatedOn { get; set; }
    }
}
