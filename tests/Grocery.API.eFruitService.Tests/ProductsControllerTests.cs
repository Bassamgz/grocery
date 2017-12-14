namespace Grocery.API.eFruitService.Tests
{
    using System.Linq;
    using AutoMapper;
    using Grocery.API.eFruitService.Controllers;
    using Grocery.Core.Data.Model.DAO;
    using Grocery.Core.Service;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class ProductsControllerTests
    {
        private readonly Mock<IMapper> autoMapper = new Mock<IMapper>();
        private readonly Mock<IProductService> productService = new Mock<IProductService>();

        [Fact]
        public void GetAllProducts_Empty_ReturnsOK()
        {
            // Arrange
            productService.Setup(repo => repo.GetAllProducts()).Returns(Enumerable.Empty<Product>());
            var controller = new ProductsController(productService.Object, autoMapper.Object);

            // Act
            var result = controller.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllProducts_Null_ReturnsNotFound()
        {
            // Arrange
            productService.Setup(repo => repo.GetAllProducts()).Returns(() => null);
            var controller = new ProductsController(productService.Object, autoMapper.Object);

            // Act
            var result = controller.GetAllProducts();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
