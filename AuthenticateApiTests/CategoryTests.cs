using AuthenticateApiClassLibrary.Data;
namespace AuthenticateApiTests;


[TestClass]
public class CategoryTest
{
    [TestMethod]
    public void Category_ShouldHaveProperties_IdAndDescription()
    {
        // Arrange
        var category = new Category();

        // Act and Assert
        Assert.IsNotNull(category.Id);
        Assert.IsNotNull(category.Description);
    }
    // Tests that the ToString method of the Category class 
    //overrides the default behavior and includes the Description property in the output.
    [TestMethod]
    public void Category_ShouldHaveExpectedDefaultValue_ForIdAndDescription()
    {
        // Arrange
        var category = new Category();

        // Act and Assert
        Assert.AreEqual(0, category.Id);
        Assert.AreEqual(string.Empty, category.Description);
    }

    /*Tests ToString method of the Category class 
    overrides the default behavior and 
    includes the Description property in the output.
    */
    [TestMethod]
    public void Category_ShouldHaveExpectedValues_ForIdAndDescription()
    {
        // Arrange
        var id = 1;
        var description = "Vegetables";
        var category = new Category() { Id = id, Description = description };

        // Act and Assert
        Assert.AreEqual(id, category.Id);
        Assert.AreEqual(description, category.Description);
    }
    /* Tests ToString method of the Category class 
     overrides the default behavior and 
     includes the Description property in the output.
    */
    [TestMethod]
    public void Category_ShouldOverrideToString_ToIncludeDescription()
    {
        // Arrange
        var description = "Liquors";
        var category = new Category() { Description = description };

        // Act
        var result = category.ToString();

        // Assert
        Assert.AreEqual(description, result);
    }
}