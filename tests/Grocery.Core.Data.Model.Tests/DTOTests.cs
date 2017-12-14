namespace Grocery.Core.Data.Model.Tests
{
    using System;
    using Grocery.Core.Data.Model.DTO;
    using Xunit;

    public class DTOTests
    {
        private readonly int cartId = 1;
        private readonly int productId = 1;
        private readonly int quantity = 1;
        private readonly decimal price = 10;
        private readonly string orderId = "15236";
        private readonly string productName = "Test";
        private readonly int customerId = 24;
        private readonly DateTime date = new DateTime(2017, 12, 11);

        [Fact]
        public void Cart_Correct_ObjectCreated()
        {
            // Arrange
            var cart = new Cart
            {
                Id = this.cartId,
                CreatedOn = this.date,
                TotalCost = this.price
            };

            // Act

            // Assert
            Assert.Equal(this.cartId, cart.Id);
            Assert.Equal(this.date.ToShortDateString(), cart.CreatedOn.ToShortDateString());
            Assert.Equal(this.price, cart.TotalCost);
        }

        [Fact]
        public void CartItem_Correct_ObjectCreated()
        {
            // Arrange
            var cartItem = new CartItem
            {
                ProductId = this.productId,
                CartId = this.cartId,
                AddedOn = this.date,
                Quantity = this.quantity,
                Cost = this.price
            };

            // Act

            // Assert
            Assert.Equal(this.productId, cartItem.ProductId);
            Assert.Equal(this.cartId, cartItem.CartId);
            Assert.Equal(this.date.ToShortDateString(), cartItem.AddedOn.ToShortDateString());
            Assert.Equal(this.price, cartItem.Cost);
            Assert.Equal(this.quantity, cartItem.Quantity);
        }

        [Fact]
        public void CustomerCart_Correct_ObjectCreated()
        {
            // Arrange
            var customerCart = new CustomerCart
            {
                CartId = this.cartId,
                CustomerId = this.customerId
            };

            // Act

            // Assert
            Assert.Equal(this.cartId, customerCart.CartId);
            Assert.Equal(this.customerId, customerCart.CustomerId);
        }

        [Fact]
        public void Order_Correct_ObjectCreated()
        {
            // Arrange
            var order = new Order
            {
                Id = this.orderId,
                PurchasedOn = this.date,
                TotalCost = this.price
            };

            // Act

            // Assert
            Assert.Equal(this.orderId, order.Id);
            Assert.Equal(this.date.ToShortDateString(), order.PurchasedOn.ToShortDateString());
            Assert.Equal(this.price, order.TotalCost);
        }

        [Fact]
        public void OrderItem_Correct_ObjectCreated()
        {
            // Arrange
            var orderItem = new OrderItem
            {
                OrderId = this.orderId,
                Quantity = this.quantity,
                Cost = this.price
            };

            // Act

            // Assert
            Assert.Equal(this.orderId, orderItem.OrderId);
            Assert.Equal(this.price, orderItem.Cost);
            Assert.Equal(this.quantity, orderItem.Quantity);
        }

        [Fact]
        public void CustomerOrder_Correct_ObjectCreated()
        {
            // Arrange
            var customerOrder = new CustomerOrder
            {
                OrderId = this.orderId,
                CustomerId = this.customerId
            };

            // Act

            // Assert
            Assert.Equal(this.orderId, customerOrder.OrderId);
            Assert.Equal(this.customerId, customerOrder.CustomerId);
        }

        [Fact]
        public void Product_Correct_ObjectCreated()
        {
            // Arrange
            var product = new Product
            {
                Id = this.productId,
                Name = this.productName,
                Price = this.price
            };

            // Act

            // Assert
            Assert.Equal(this.productId, product.Id);
            Assert.Equal(this.productName, product.Name);
            Assert.Equal(this.price, product.Price);
        }
    }
}
