using Catalog.API.Data;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext context;
        public ProductRepository(ICatalogContext context)
        {
            this.context = context;
        }

        public async Task CreateProduct(Product product)
        {
            await context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var status = await context.Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return status.IsAcknowledged && status.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(f => f.Id, id);
            var status = await context.Products.DeleteOneAsync(filter);
            return status.IsAcknowledged && status.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id) => await context.Products.Find(f => f.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(f => f.Category, categoryName);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(f => f.Name, name);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result = await context.Products.Find(f => true).ToListAsync();
            return result;
        }
    }
}
