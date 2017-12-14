namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;

    public class OrderItemsRepository : RepositoryBase<OrderItem>, IOrderItemsRepository
    {
        public OrderItemsRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        /// <summary>
        /// Get order Items by Order Id
        /// </summary>
        /// <param name="orderId">Order reference</param>
        /// <returns>IQueryable of order items</returns>
        public async Task<IQueryable<OrderItem>> GetOrderItemsAsync(string orderId)
        {
            var order = await DbContext.Order.AsNoTracking().
                FirstOrDefaultAsync(item => item.Id == orderId);

            return order.OrderItem.AsQueryable();
        }
    }
}
