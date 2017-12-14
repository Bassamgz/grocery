namespace Grocery.Core.Data.Model.DAO
{
    public class CustomerOrder
    {
        public int CustomerId { get; set; }

        public string OrderId { get; set; }

        // Navigations Lazy Loading
        public virtual Order Order { get; set; }

        public virtual ApplicationUser Customer { get; set; }
    }
}
