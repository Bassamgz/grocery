namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public interface ICustomerOrderRepository : IRepository<CustomerOrder>
    {
        Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, string orderId);

        Task<IQueryable<Order>> GetCustomerOrdersAsync(string customerEmail);
    }
}
