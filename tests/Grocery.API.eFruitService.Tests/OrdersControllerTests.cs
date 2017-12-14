namespace Grocery.API.eFruitService.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Grocery.API.eFruitService.Controllers;
    using Grocery.Core.Data.Model.DAO;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class OrdersControllerTests
    {
        private readonly Mock<IMapper> autoMapper = new Mock<IMapper>();
        private readonly Mock<IOrderService> orderService = new Mock<IOrderService>();
        private readonly Mock<ICartService> cartService = new Mock<ICartService>();

        private readonly string customerEmail = "test@test.com";
        private readonly string orderId = Guid.NewGuid().ToString();

        [Fact]
        public async void GetOrderItemsAsync_Empty_ReturnsOK()
        {
            // Arrange
            orderService.Setup(repo => repo.GetOrderItemsAsync(It.IsAny<string>())).Returns(Task.FromResult(Enumerable.Empty<OrderItem>().AsQueryable()));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.GetOrderItemsAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetOrderItemsAsync_Null_ReturnsNotFound()
        {
            // Arrange
            orderService.Setup(repo => repo.GetOrderItemsAsync(It.IsAny<string>())).Returns(Task.FromResult<IQueryable<OrderItem>>(null));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.GetOrderItemsAsync(customerEmail);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCustomerOrderAsync_Empty_ReturnsOK()
        {
            // Arrange
            orderService.Setup(repo => repo.GetCustomerOrderAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new CustomerOrder()));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.GetCustomerOrderAsync(customerEmail, orderId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCustomerOrderAsync_Null_ReturnsNotFound()
        {
            // Arrange
            orderService.Setup(repo => repo.GetCustomerOrderAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<CustomerOrder>(null));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.GetCustomerOrderAsync(customerEmail, orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCustomerOrdersAsync_Null_ReturnsNotFound()
        {
            // Arrange
            orderService.Setup(repo => repo.GetCustomerOrdersAsync(It.IsAny<string>())).Returns(Task.FromResult<IQueryable<Order>>(null));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.GetCustomerOrdersAsync(customerEmail);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PlaceOrderAsync_HasValue_ReturnsOk()
        {
            // Arrange
            orderService.Setup(repo => repo.PlaceOrderAsync(It.IsAny<string>())).Returns(Task.FromResult(orderId));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.PlaceOrderAsync(customerEmail);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void PlaceOrderAsync_Empty_ReturnsNotFound()
        {
            // Arrange
            orderService.Setup(repo => repo.PlaceOrderAsync(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
            var controller = new OrdersController(orderService.Object, cartService.Object, autoMapper.Object);

            // Act
            var result = await controller.PlaceOrderAsync(customerEmail);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}