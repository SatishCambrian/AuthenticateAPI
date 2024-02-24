using AuthenticateApiClassLibrary.Data;
using Authenticated.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticateWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;

        public ProductController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        //Get method->returns all products 

        [HttpGet("/")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var allProducts = appDbContext.Products.Select(x => new
            {
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Category = x.ProductCategory
            }).ToList();

            if (allProducts.Count == 0)
            {
                return NotFound("No products found.");
            }

            return Ok(allProducts);
        }


        //Add a Get endpoint that takes a category Id and returns all products in that category. 
        //returns  products specified in category
        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var productsInCategory = appDbContext.Products
                .Where(p => p.ProductCategory.Id == categoryId)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.ProductCategory
                })
                .ToList();

            if (productsInCategory.Count == 0)
            {
                return NotFound("ID not found in category " + categoryId);
            }
            return Ok(productsInCategory);
        }


        //Add a Post endpoint that takes a single product and adds it to the database.
        [HttpPost("add")]
        public ActionResult AddProduct([FromBody] Product productToDatabase)
        {
            // Get the product category based on the description
            var category = GetCategory(productToDatabase.ProductCategory.Description);
            // Create a new product
            var productToAdd = new Product
            {
                Name = productToDatabase.Name,
                Price = productToDatabase.Price,
                Description = productToDatabase.Description,
                ProductCategory = category,
            };
            // Add the product to the database
            appDbContext.Products.Add(productToAdd);
            appDbContext.SaveChanges();

            // Return the name of the product added
            return Ok(productToAdd.Name + " added successfully");
        }

        private Category GetCategory(string description)
        {
            var category = appDbContext.Categories.FirstOrDefault(c => c.Description == description);

            if (category == null)
            {
                category = new Category { Description = description };
                appDbContext.Categories.Add(category);
            }

            return category;
        }

    }
}