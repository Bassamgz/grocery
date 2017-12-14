namespace Grocery.Core.Data.Repositories
{
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
