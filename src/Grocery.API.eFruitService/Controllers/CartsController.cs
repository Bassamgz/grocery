namespace Grocery.API.eFruitService.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/carts")]
    public class CartsController : Controller
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartsController(ICartService cartService, IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }

        // GET api/carts/5
        [AcceptVerbs("GET")]
        [Route("{cartId:int}")]
        public IActionResult GetCartItems(int cartId)
        {
            var cartItems = cartService.GetCartItems(cartId);
            if (cartItems == null)
            {
                return NotFound();
            }

            var dtoCartItems = cartItems.ProjectTo<CartItem>();
            return Ok(dtoCartItems);
        }

        // GET api/carts/b@c.com
        [AcceptVerbs("GET")]
        [Route("{customerEmail}")]
        public IActionResult GetCustomerCartItems(string customerEmail)
        {
            var cartItems = cartService.GetCustomerCartItems(customerEmail);
            if (cartItems == null)
            {
                return NotFound();
            }

            var dtoCartItems = cartItems.ProjectTo<CartItem>();
            return Ok(dtoCartItems);
        }

        // POST api/carts/b@c.com/3
        [AcceptVerbs("POST")]
        [Route("{customerEmail}/{productId:int}")]
        public void PostCartItem(string customerEmail, int productId)
        {
            cartService.AddCartItem(customerEmail, productId);
        }

        // POST api/carts/b@c.com
        [AcceptVerbs("POST")]
        [Route("{customerEmail}")]
        public void PostCreateCart(string customerEmail)
        {
            cartService.CreateCart(customerEmail);
        }

        // POST api/carts/b@c.com/3
        [AcceptVerbs("DELETE")]
        [Route("{customerEmail}/{productId:int}")]
        public void DeleteCartItem(string customerEmail, int productId)
        {
            cartService.DeleteCartItem(customerEmail, productId);
        }
    }
}