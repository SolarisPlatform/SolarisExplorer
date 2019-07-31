using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Models.CoinDataService;

namespace Solaris.BlockExplorer.UI.Services
{
    public class CoinDataService : ICoinDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        public CoinDataService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }

        public async Task<ICoinData> GetCoinData()
        {
            if (!_memoryCache.TryGetValue("CoinGecko", out ICoinData coinData))
            {
                var client = _httpClientFactory.CreateClient("CoinGecko");
                var queryString = "?tickers=true&market_data=true";
                var result = await client.GetStringAsync(queryString);

                return _memoryCache.Set("CoinGecko", JsonConvert.DeserializeObject<CoinData>(result), DateTime.UtcNow.AddMinutes(15));
            }

            return coinData;
        }
    }
}
