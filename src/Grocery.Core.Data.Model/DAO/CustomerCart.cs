namespace Grocery.Core.Data.Model.DAO
{
    public class CustomerCart
    {
        public int CustomerId { get; set; }

        public int CartId { get; set; }

        // Navigations Lazy Loading
        public virtual Cart Cart { get; set; }

        public virtual ApplicationUser Customer { get; set; }
    }
}
