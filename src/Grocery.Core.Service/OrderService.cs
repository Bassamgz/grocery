namespace Grocery.Core.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Grocery.Core.Data.Infrastructure;
    using Grocery.Core.Data.Model.DAO;
    using Grocery.Core.Data.Repositories;

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly ICustomerOrderRepository customerOrderRepository;
        private readonly ICartItemsRepository cartItemsRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemsRepository orderItemsRepository,
            ICustomerOrderRepository customerOrderRepository,
            ICartItemsRepository cartItemsRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.orderRepository = orderRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.customerOrderRepository = customerOrderRepository;
            this.cartItemsRepository = cartItemsRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IQueryable<OrderItem>> GetOrderItemsAsync(string orderId)
        {
            var orderItems = await orderItemsRepository.GetOrderItemsAsync(orderId);
            return orderItems;
        }

        public async Task<CustomerOrder> GetCustomerOrderAsync(string customerEmail, string orderId)
        {
            var customerOrder = await customerOrderRepository.GetCustomerOrderAsync(customerEmail, orderId);
            return customerOrder;
        }

        public async Task<IQueryable<Order>> GetCustomerOrdersAsync(string customerEmail)
        {
            var customerOrders = await customerOrderRepository.GetCustomerOrdersAsync(customerEmail);
            return customerOrders;
        }

        public async Task<string> PlaceOrderAsync(string customerEmail)
        {
            // Get Cart Items Id
            var cartItems = cartItemsRepository.GetCartItems(customerEmail);

            // Get customer
            var customer = userRepository.Get(item => item.Email == customerEmail);

            // Get Cart Cost and Product Ids
            decimal cost = 0;
            List<string> productIds = new List<string>();
            foreach (var item in cartItems)
            {
                cost += item.Cost * item.Quantity;
                productIds.AddRange(
                    Enumerable.Repeat(
                        item.ProductId.ToString(),
                    item.Quantity).ToList());
            }

            var placeOrderService = new Services.OrderServiceClient();
            var orderRef = await placeOrderService.PlaceAsync(
                new Services.Order
                {
                    CustomerName = customerEmail,
                    Total = cost,
                    ItemIds = productIds.ToArray()
                });

            if (!string.IsNullOrEmpty(orderRef))
            {
                // Add order data
                var order = new Order
                {
                    Id = orderRef,
                    TotalCost = cost,
                    OrderItem = new List<OrderItem>(
                        cartItems.Select(item =>
                        new OrderItem
                        {
                            ProductId = item.ProductId,
                            Cost = item.Cost,
                            Quantity = item.Quantity,
                            OrderId = orderRef
                        }).ToList()),
                    CustomerOrder = new List<CustomerOrder>
                    {
                        new CustomerOrder { CustomerId = customer.Id, OrderId = orderRef }
                    }
                };

                orderRepository.Add(order);

                // Remove cart items
                cartItemsRepository.EmptyCart(customerEmail);

                Save();
            }

            return orderRef;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
