using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        IMongoDatabase Database { get; set; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            Database = client.GetDatabase("CatalogDb");

            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products => Database.GetCollection<Product>("Products");
    }
}
