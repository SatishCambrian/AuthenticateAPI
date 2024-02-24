using AuthenticateApiClassLibrary.Data;
namespace AuthenticateApiTests;


    [TestClass]
    public class AuthenticatedAPITestCaseForProduct
    {

        [TestMethod]
        public void Product_ShouldSetProperties()
        {
            var product = new Product
            {
                Id = 1,
                Name = "papaya",
                Price = 2.0m,
                Description = "Product papaya",
                ProductCategory = new Category { Id = 1, Description = "Fruit" }
            };


            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("papaya", product.Name);
            Assert.AreEqual(2.0m, product.Price);
            Assert.AreEqual("product papaya", product.Description);
            Assert.IsNotNull(product.ProductCategory);
            Assert.AreEqual(1, product.ProductCategory.Id);
            Assert.AreEqual("Fruit", product.ProductCategory.Description);
        }

        [TestMethod]
        public void Product_IdShouldBeAnInteger()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Hammer",
                Price = 23.0m,
                Description = "Machinery",
                ProductCategory = new Category { Id = 1, Description = "Equipment" }
            };

            Assert.IsInstanceOfType(product.Id, typeof(int), "Id should be an instance of Int"); 

        }

        [TestMethod]
        public void Product_PriceShouldBeAnDecimal()
        {
            var product = new Product
            {
                Id = 3,
                Name = "Guava",
                Price = 20.0m,
                Description = "Fruit",
                ProductCategory = new Category { Id = 3, Description = "Fruit" }
            };

            Assert.IsInstanceOfType(product.Price, typeof(decimal), "Price should be an instance of decimal");

        }

        [TestMethod]
        public void Product_ProductCategoryShouldBeInstanceOfCategory()
        {
            var product = new Product
            {
                Id = 1,
                Name = "mango",
                Price = 2.0m,
                Description = "fresh mango",
                ProductCategory = new Category { Id = 1, Description = "Fruit" }
            };


            Assert.IsInstanceOfType(product.ProductCategory, typeof(Category), "Product Category should be an instance of Category");
            
        }

    }
