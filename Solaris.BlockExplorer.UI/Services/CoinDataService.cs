using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Solaris.BlockExplorer.UI.Models;

namespace Solaris.BlockExplorer.UI.Services
{
    public class CoinDataService : ICoinDataService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CoinDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CoinData.RootObject> GetCoinData()
        {
            var client = _httpClientFactory.CreateClient("CoinGecko");
            var queryString = $"?tickers=true&market_data=true";
            var result = await client.GetStringAsync(queryString);
            return JsonConvert.DeserializeObject<CoinData.RootObject>(result);
            
        }
    }
}
