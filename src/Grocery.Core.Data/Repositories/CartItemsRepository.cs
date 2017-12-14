namespace Grocery.Core.Data.Repositories
{
    using System.Linq;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Microsoft.EntityFrameworkCore;

    public class CartItemsRepository : RepositoryBase<CartItem>, ICartItemsRepository
    {
        public CartItemsRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        /// <summary>
        /// Add product to a customer cart
        /// </summary>
        /// <param name="customerEmail">Customer's email</param>
        /// <param name="productId">Added product Id</param>
        public void AddCartItem(string customerEmail, int productId)
        {
            var customer = DbContext.Users.AsNoTracking().
                FirstOrDefault(user => user.Email == customerEmail);

            var customerCart = DbContext.CustomerCart.
                Include(i => i.Cart).
                FirstOrDefault(item => item.CustomerId == customer.Id);

            var product = DbContext.Product.AsNoTracking().
                FirstOrDefault(item => item.Id == productId);

            // If item exists, Increase Quantity and totals
            var existingItem = DbContext.CartItem.
                Include(i => i.Cart).
                FirstOrDefault(item => item.ProductId == productId && item.CartId == customerCart.CartId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.Cost += product.Price;
                existingItem.Cart.TotalCost += product.Price;
                Update(existingItem);
            }
            else
            {
                // If new item, add and increase totals
                customerCart.Cart.TotalCost += product.Price;
                Add(new CartItem { CartId = customerCart.CartId, ProductId = productId, Quantity = 1, Cost = product.Price });
            }
        }

        /// <summary>
        /// Delete cart item for given customer
        /// </summary>
        /// <param name="customerEmail">Customer's email</param>
        /// <param name="productId">To be removed product Id</param>
        public void DeleteCartItem(string customerEmail, int productId)
        {
            var customer = DbContext.Users.AsNoTracking().
                FirstOrDefault(user => user.Email == customerEmail);

            var customerCart = DbContext.CustomerCart.
                FirstOrDefault(item => item.CustomerId == customer.Id);

            var product = DbContext.Product.AsNoTracking().
                FirstOrDefault(item => item.Id == productId);

            // If item exists with quantity > 1, Decrease Quantity
            var existingItem = DbContext.CartItem.Include(i => i.Cart).
                FirstOrDefault(item => item.ProductId == productId && item.CartId == customerCart.CartId);

            if (existingItem != null && existingItem.Quantity > 1)
            {
                existingItem.Quantity--;
                existingItem.Cost -= product.Price;
                existingItem.Cart.TotalCost -= product.Price;
                Update(existingItem);
            }
            else
            {
                // If one item, remove and decrease totals
                customerCart.Cart.TotalCost -= product.Price;
                Delete(existingItem);
            }
        }

        /// <summary>
        /// Get cart items of a given cart
        /// </summary>
        /// <param name="cartId">Cart Id</param>
        /// <returns>IQueryable of CartItem</returns>
        public IQueryable<CartItem> GetCartItems(int cartId)
        {
            var cartItems = DbContext.CartItem.AsNoTracking().
                Where(item => item.CartId == cartId);

            return cartItems;
        }

        /// <summary>
        /// Get cart items of a given user
        /// </summary>
        /// <param name="customerEmail">Customer Email</param>
        /// <returns>IQueryable of CartItem</returns>
        public IQueryable<CartItem> GetCartItems(string customerEmail)
        {
            IQueryable<CartItem> cartItems = null;
            var customer = DbContext.Users.AsNoTracking().
                FirstOrDefault(user => user.Email == customerEmail);

            if (customer != null)
            {
                var customerCart = DbContext.CustomerCart.AsNoTracking().
                    FirstOrDefault(item => item.CustomerId == customer.Id);

                if (customerCart != null)
                {
                    cartItems = DbContext.CartItem.AsNoTracking().
                        Include(i => i.Cart).
                        Where(item => item.CartId == customerCart.CartId);
                }
            }

            return cartItems;
        }

        /// <summary>
        /// Empty cart items for given customer
        /// </summary>
        /// <param name="customerEmail">Customer Email</param>
        public void EmptyCart(string customerEmail)
        {
            var customer = DbContext.Users.AsNoTracking().
                FirstOrDefault(user => user.Email == customerEmail);

            var customerCart = DbContext.CustomerCart.
                FirstOrDefault(item => item.CustomerId == customer.Id);

            // Delete all and reset totals
            Delete(item => item.CartId == customerCart.CartId);
            customerCart.Cart.TotalCost = 0;
        }
    }
}
