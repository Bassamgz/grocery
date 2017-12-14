namespace Grocery.Core.Data.Repositories
{
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public interface ICustomerCartRepository : IRepository<CustomerCart>
    {
        Task<CustomerCart> GetCustomerCartAsync(string customerEmail);
    }
}
