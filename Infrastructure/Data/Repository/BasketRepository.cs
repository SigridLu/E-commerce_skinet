using System.Text.Json;
using Core.Contract.Repository;
using Core.Entities;
using StackExchange.Redis;

namespace Infrastructure.Data.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            // Basket stores data in string, we need to deserialize customer json data into string before storing in redis.
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        // Create and Update basket uses same method, if the basket is already exist, overwrite the existing one with the new data.
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            // Keep the basket for 30 days
            var created = await _database.StringSetAsync(basket.Id, 
            JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}