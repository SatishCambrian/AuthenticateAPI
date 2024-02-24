using AuthenticateApiClassLibrary.Data;
using AuthenticateWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace AuthenticateApiTests;

[TestClass]
public class ShoppingCartTests
{
       [TestMethod]
        public void ShoppingCart_ShouldHaveProperties_WhenInstantiated()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
        // Act
        var properties = typeof(ShoppingCart).GetProperties();

        // Assert
        Assert.AreEqual(3, properties.Length);
        Assert.IsTrue(properties.Contains(x => x.Name == "Id"));
        Assert.IsTrue(properties.Contains(x => x.Name == "User"));
        Assert.IsTrue(properties.Contains(x => x.Name == "Products"));
    }
[TestMethod]
        public void RemoveProductFromShoppingCart_RemovesProductFromCart_ReturnsOk()
        {
            // Arrange
            var productId = 1;
            var controller = new ShoppingCartController(context);

            // Act
            var result = controller.RemoveProductFromShoppingCart(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RemoveProductFromShoppingCartResponse>(okResult.Value);
            Assert.Equal(productId, response.ProductId);
            Assert.Equal("Product removed. Items in cart 1, now there is 0 items remaining", response.Message);
        }
}