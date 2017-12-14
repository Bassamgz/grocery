namespace Grocery.Core.Data.Repositories
{
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;

    public class CustomerCartRepository : RepositoryBase<CustomerCart>, ICustomerCartRepository
    {
        public CustomerCartRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        /// <summary>
        /// Get a customer cart of a given customer
        /// </summary>
        /// <param name="customerEmail">Customer's Email</param>
        /// <returns>A Customer cart</returns>
        public async Task<CustomerCart> GetCustomerCartAsync(string customerEmail)
        {
            var customer = await DbContext.Users.AsNoTracking().
                FirstOrDefaultAsync(user => user.Email == customerEmail);

            var customerCart = await DbContext.CustomerCart.AsNoTracking().
                FirstOrDefaultAsync(item => item.CustomerId == customer.Id);

            return customerCart;
        }
    }
}
