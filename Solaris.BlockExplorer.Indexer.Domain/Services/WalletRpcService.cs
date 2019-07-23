using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Solaris.BlockExplorer.Indexer.Domain.Models;

namespace Solaris.BlockExplorer.Indexer.Domain.Services
{
    public class WalletRpcService<T> : IWalletRpcService<T>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WalletRpcService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> Request(string method, params object[] parameters)
        {
            var httpClient = _httpClientFactory.CreateClient("Daemon");

            var jsonRpcObject = new JObject
            {
                ["jsonrpc"] = "1.0",
                ["id"] = "1",
                ["method"] = method
            };

            if (parameters != null && parameters.Length > 0)
            {
                var properties = new JArray();
                foreach (var parameter in parameters)
                {
                    properties.Add(parameter);
                }
                jsonRpcObject.Add(new JProperty("params", properties));
            }

            return await MakeRequest(method, jsonRpcObject, httpClient);
        }

        private static async Task<T> MakeRequest(string method, JObject jsonRpcObject, HttpClient httpClient)
        {
            var requestJson = JsonConvert.SerializeObject(jsonRpcObject);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress)
            {
                Content = new StringContent(requestJson, Encoding.ASCII, "application/json-rpc"),
                Version = new Version(1, 1, 0, 0)
            };

            var responseMessage = await httpClient.SendAsync(httpRequestMessage);

            responseMessage.EnsureSuccessStatusCode();

            var walletRpcResult = await responseMessage.Content.ReadAsAsync<WalletRpcResult<T>>();

            return walletRpcResult.Result;
        }
    }
}
