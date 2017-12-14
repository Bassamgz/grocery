namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public interface IOrderItemsRepository : IRepository<OrderItem>
    {
        Task<IQueryable<OrderItem>> GetOrderItemsAsync(string orderId);
    }
}
