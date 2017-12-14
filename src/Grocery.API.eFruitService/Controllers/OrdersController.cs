namespace Grocery.API.eFruitService.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Grocery.Core.Data.Model.DTO;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public OrdersController(
            IOrderService orderService,
            ICartService cartService,
            IMapper mapper)
        {
            this.orderService = orderService;
            this.cartService = cartService;
            this.mapper = mapper;
        }

        // GET api/orders/0c2dbd8f-3d64-4ee4-91ec-9ed7e77d0d7e
        [AcceptVerbs("GET")]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrderItemsAsync(string orderId)
        {
            var orderItems = await orderService.GetOrderItemsAsync(orderId);
            if (orderItems == null)
            {
                return NotFound();
            }

            var dtoOrderItems = mapper.Map<IQueryable<Core.Data.Model.DAO.OrderItem>, IQueryable<OrderItem>>(orderItems);
            return Ok(dtoOrderItems);
        }

        // GET api/orders/k@hotel.com/0c2dbd8f-3d64-4ee4-91ec-9ed7e77d0d7e
        [AcceptVerbs("GET")]
        [Route("{customerEmail}/{orderId}")]
        public async Task<IActionResult> GetCustomerOrderAsync(string customerEmail, string orderId)
        {
            var customerOrder = await orderService.GetCustomerOrderAsync(customerEmail, orderId);
            if (customerOrder == null)
            {
                return NotFound();
            }

            var dtoCustomerOrder = mapper.Map<Core.Data.Model.DAO.CustomerOrder, CustomerOrder>(customerOrder);
            return Ok(dtoCustomerOrder);
        }

        // GET api/orders/k@hotel.com
        [AcceptVerbs("GET")]
        [Route("{customerEmail}")]
        public async Task<IActionResult> GetCustomerOrdersAsync(string customerEmail)
        {
            var customerOrders = await orderService.GetCustomerOrdersAsync(customerEmail);
            if (customerOrders == null)
            {
                return NotFound();
            }

            var dtoCustomerOrders = customerOrders.ProjectTo<Order>();
            return Ok(dtoCustomerOrders);
        }

        // POST api/orders/k@hotel.com
        [AcceptVerbs("POST")]
        [Route("{customerEmail}")]
        public async Task<IActionResult> PlaceOrderAsync(string customerEmail)
        {
            var orderRef = await orderService.PlaceOrderAsync(customerEmail);
            if (string.IsNullOrEmpty(orderRef))
            {
                return NotFound();
            }

            return Ok(orderRef);
        }
    }
}