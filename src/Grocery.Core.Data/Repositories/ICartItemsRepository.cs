namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;

    public interface ICartItemsRepository : IRepository<CartItem>
    {
        IQueryable<CartItem> GetCartItems(int cartId);

        IQueryable<CartItem> GetCartItems(string customerEmail);

        void AddCartItem(string customerEmail, int productId);

        void DeleteCartItem(string customerEmail, int productId);

        void EmptyCart(string customerEmail);
    }
}
