using StackExchange.Redis;
using Store.G01.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G01.Service.Services.Cashes
{

    public class CasheService : ICasheService
    {
        private readonly IDatabase _database;
        public CasheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCasheKeyAsync(string key)
        {
            var casheResponse = await _database.StringGetAsync(key);
            if (casheResponse.IsNullOrEmpty) return null;
            return casheResponse.ToString();
        }

        public async Task SetCasheKeyAsync(string Key, object response, TimeSpan expireTime)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await _database.StringSetAsync(Key, JsonSerializer.Serialize(response, options), expireTime);
        }
    }
}
