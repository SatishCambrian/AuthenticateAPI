using AuthenticateApiClassLibrary.Data;
using Authenticated.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticateWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;

        public ShoppingCartController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        //Get all products in the shopping cart

        [HttpGet("shopcart")]
        public ActionResult<IEnumerable<Product>> GetProductsShoppingCart()
        {
            //gets the email of the currently authenticated user
            var userId = User.FindFirst(ClaimTypes.Email)?.Value;
            var shopCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
            //filter the shopping cart by user email;
                .FirstOrDefault(sc => sc.User == userId);

            if (shopCart == null)
            {
                return NotFound("ShopCart is not been created.");
            }

            return Ok(shopCart.Products);
        }


        /* 
        Add a Post endpoint that takes a single ID and removes the item from the shopping cart.	
        */

        // Removes a product with a given ID from the shopping cart.
        [HttpPost("shopcart/remove-product/{productId}")]
        public ActionResult RemoveProductFromShoppingCart(int productId)
        {
            //gets the email of the currently authenticated user
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var shopCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
            //filter the shopping cart by user email;
                .FirstOrDefault(sc => sc.User == email);
            var count = shopCart?.Products?.Count ?? 0;

            if (shopCart == null)
            {
                return NotFound("ShoppingCart not found");
            }
            // find the product to remove
            var productRemove = shopCart.Products.FirstOrDefault(p => p.Id == productId);

            if (productRemove == null)
            {
                return NotFound("Product Id " + productId + " not found on Cart");
            }
            shopCart.Products.Remove(productRemove);
            appDbContext.SaveChanges();

            return Ok($"Product removed.Items in cart {count}, now there is {GetProductsShoppingCart(shopCart.Id)} items remaining");
        }

        // Adds a product with a given ID to the shopping cart.

        [HttpPost("shopcart/add-product/{productId}")]
        public ActionResult AddProductToShoppingCart(int productId)
        {
            //gets the email of the currently authenticated user
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var shopCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
            //filter the shopping cart by user email;
                .FirstOrDefault(sc => sc.User == userEmail);

            // creates a new shopping cart if one does not exist for the current user
            if (shopCart == null)
            {
                shopCart = new ShoppingCart
                {
                    User = userEmail ?? "",
                    Products = new List<Product>()
                };
                appDbContext.ShoppingCarts.Add(shopCart);
            }
            // finds the product to add to the shopping cart
            var productAdd = appDbContext.Products.Find(productId);

            if (productAdd == null)
            {
                return NotFound("Product not found");
            }
            // adds the product to the shopping cart
            shopCart.Products.Add(productAdd);
            appDbContext.SaveChanges();

            // returns a message product was added to the cart
            return Ok("Product " + productAdd.Name + " added successfully in the cart. \nShopping cart has " + GetProductsShoppingCart(shopCart.Id) + " products.");
        }

        //get the number of products in your shopping cart
        private int GetProductsShoppingCart(int shoppingCartId)
        {
            var count = appDbContext.ShoppingCarts
                        .Where(cart => cart.Id == shoppingCartId)
                        .SelectMany(cart => cart.Products)
                        .Count();

                return count;
        }
    }
}