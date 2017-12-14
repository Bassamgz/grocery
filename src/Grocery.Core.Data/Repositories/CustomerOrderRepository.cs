namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;

    public class CustomerOrderRepository : RepositoryBase<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        /// <summary>
        /// Get customer order by customer email and order id
        /// </summary>
        /// <param name="customerEmail">Customer email</param>
        /// <param name="orderId">Order Id</param>
        /// <returns>Customer order</returns>
        public async Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, string orderId)
        {
            var customer = await DbContext.Users.AsNoTracking().
                FirstOrDefaultAsync(user => user.Email == customerEmail);

            var customerOrder = await DbContext.CustomerOrder.AsNoTracking().
                FirstOrDefaultAsync(item => item.CustomerId == customer.Id && item.OrderId == orderId);

            return customerOrder;
        }

        /// <summary>
        /// Returns all orders for given customer
        /// </summary>
        /// <param name="customerEmail">Customer email</param>
        /// <returns>IQueryable of customer's orders</returns>
        public async Task<IQueryable<Order>> GetCustomerOrdersAsync(string customerEmail)
        {
            var customer = await DbContext.Users.AsNoTracking().
                FirstOrDefaultAsync(user => user.Email == customerEmail);

            var customerOrders = DbContext.CustomerOrder.AsNoTracking().
                Where(item => item.CustomerId == customer.Id).
                Select(item => item.Order).
                OrderBy(o => o.PurchasedOn);

            return customerOrders;
        }
    }
}
