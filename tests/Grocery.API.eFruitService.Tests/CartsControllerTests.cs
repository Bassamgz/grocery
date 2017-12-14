namespace Grocery.API.eFruitService.Tests
{
    using AutoMapper;
    using Grocery.API.eFruitService.Controllers;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class CartsControllerTests
    {
        private readonly Mock<IMapper> autoMapper = new Mock<IMapper>();
        private readonly Mock<ICartService> cartService = new Mock<ICartService>();
        private readonly int cartId = 1;
        private readonly string customerEmail = "Test@test.com";

        [Fact]
        public void GetCartItems_Null_ReturnsNotFound()
        {
            // Arrange
            cartService.Setup(repo => repo.GetCartItems(It.IsAny<int>())).Returns(() => null);
            var controller = new CartsController(cartService.Object, autoMapper.Object);

            // Act
            var result = controller.GetCartItems(cartId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetCustomerCartItems_Null_ReturnsNotFound()
        {
            // Arrange
            cartService.Setup(repo => repo.GetCustomerCartItems(It.IsAny<string>())).Returns(() => null);
            var controller = new CartsController(cartService.Object, autoMapper.Object);

            // Act
            var result = controller.GetCustomerCartItems(customerEmail);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
