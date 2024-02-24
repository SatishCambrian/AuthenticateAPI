using AuthenticateApiClassLibrary.Data;
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

    
}