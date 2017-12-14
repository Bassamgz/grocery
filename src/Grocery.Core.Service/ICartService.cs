namespace Grocery.Core.Service
{
    using System.Linq;
    using Grocery.Core.Data.Model.DAO;

    public interface ICartService
    {
        IQueryable<CartItem> GetCartItems(int cartId);

        IQueryable<CartItem> GetCustomerCartItems(string customerEmail);

        void AddCartItem(string customerEmail, int productId);

        void DeleteCartItem(string customerEmail, int productId);

        void Save();

        void CreateCart(string customerEmail);
    }
}
