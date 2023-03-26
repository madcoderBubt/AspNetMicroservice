using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;
        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            string? basket = await _cache.GetStringAsync(userName);
            if(basket == null) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _cache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await this.GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _cache.RemoveAsync(userName);
        }
    }
}
