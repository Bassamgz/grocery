namespace Grocery.Core.Data.Model.DAO
{
    public class OrderItem
    {
        public int ProductId { get; set; }

        public string OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal Cost { get; set; }

        // Navigations Lazy Loading
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
