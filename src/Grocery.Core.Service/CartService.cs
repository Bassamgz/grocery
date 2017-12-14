namespace Grocery.Core.Service
{
    using System.Linq;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Grocery.Core.Data.Repositories;

    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartItemsRepository cartItemsRepository;
        private readonly ICustomerCartRepository customerCartRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            ICartItemsRepository cartItemsRepository,
            ICustomerCartRepository customerCartRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.cartRepository = cartRepository;
            this.cartItemsRepository = cartItemsRepository;
            this.customerCartRepository = customerCartRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddCartItem(string customerEmail, int productId)
        {
            cartItemsRepository.AddCartItem(customerEmail, productId);
            Save();
        }

        public void CreateCart(string customerEmail)
        {
            // Get customer
            var customer = userRepository.Get(item => item.Email == customerEmail);

            // Add new cart
            var newCart = new Cart { TotalCost = 0 };
            cartRepository.Add(newCart);
            Save();

            // Add CustomerCart
            customerCartRepository.Add(new CustomerCart { CartId = newCart.Id, CustomerId = customer.Id });
            Save();
        }

        public void DeleteCartItem(string customerEmail, int productId)
        {
            cartItemsRepository.DeleteCartItem(customerEmail, productId);
            Save();
        }

        public IQueryable<CartItem> GetCartItems(int cartId)
        {
            var cartItems = cartItemsRepository.GetCartItems(cartId);
            return cartItems;
        }

        public IQueryable<CartItem> GetCustomerCartItems(string customerEmail)
        {
            var customerCart = cartItemsRepository.GetCartItems(customerEmail);
            return customerCart;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
