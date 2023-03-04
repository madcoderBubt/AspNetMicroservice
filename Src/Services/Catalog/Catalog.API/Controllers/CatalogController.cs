using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger, IProductRepository productRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if(product == null)
            {
                _logger.LogError("Product with ID:{0} not found.", id);
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("[action]/{category}", Name = "GetByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _productRepository.GetProductByCategory(category);
            return Ok(product);
        }

        [HttpGet("[action]/{name}", Name = "GetByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreatePruduct(Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new {id=product.Id}, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePruduct(Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<ActionResult> DeletePruduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
