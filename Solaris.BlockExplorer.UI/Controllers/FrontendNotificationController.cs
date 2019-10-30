using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Hubs;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class FrontendNotificationController : Controller
    {
        private readonly IHubContext<FrontendNotificationHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly IBlockModelService _blockModelService;
        private readonly IMemoryCache _memoryCache;

        private static readonly object Lock = new object();

        public FrontendNotificationController(IHubContext<FrontendNotificationHub> hubContext, IConfiguration configuration, IBlockModelService blockModelService, IMemoryCache memoryCache)
        {
            _hubContext = hubContext;
            _configuration = configuration;
            _blockModelService = blockModelService;
            _memoryCache = memoryCache;
        }

        public async Task<string> Index(long height, [FromHeader(Name = "Token")] string token)
        {
            if (string.IsNullOrWhiteSpace(token) ||
                _configuration.GetValue<string>("FrontendNotificationToken") != token)
                return "";

            lock (Lock)
            {
                foreach (var key in CacheItemKeys.Keys)
                {
                    _memoryCache.Remove(key);
                }

                CacheItemKeys.Keys.Clear();
            }

            await _hubContext.Clients.All.SendAsync("wsbn", await _blockModelService.GetBlock(height));

            return "OK";
        }
    }
}