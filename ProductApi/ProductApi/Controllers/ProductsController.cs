using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{

    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop" },
        new Product { Id = 2, Name = "Phone" },
        new Product { Id = 3, Name = "Headphones" }
    };

        // 🔒 Protected endpoint – requires a valid JWT
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        // 🔓 Public endpoint – no token required
        [HttpGet("public")]
        [AllowAnonymous]
        public ActionResult<string> GetPublic()
        {
            return Ok("This is public info from Products API");
        }
    }
}
