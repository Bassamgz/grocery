namespace Grocery.Web.eFruit.Tests
{
    using Grocery.Web.eFruit.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class HomeControllerTests
    {
        private readonly Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        [Fact]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(configuration.Object);

            // Act
            var result = controller.About();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(configuration.Object);

            // Act
            var result = controller.Contact();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
