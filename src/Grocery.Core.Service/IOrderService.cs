namespace Grocery.Core.Service
{
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Model.DAO;

    public interface IOrderService
    {
        Task<IQueryable<OrderItem>> GetOrderItemsAsync(string orderId);

        Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, string orderId);

        Task<IQueryable<Order>> GetCustomerOrdersAsync(string customerEmail);

        Task<string> PlaceOrderAsync(string customerEmail);

        void Save();
    }
}
