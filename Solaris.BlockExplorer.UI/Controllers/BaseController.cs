using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IConfiguration _configuration;

        protected BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var section = _configuration.GetSection("Asset");
            var name = section.GetValue<string>("Name");
            var ticker = section.GetValue<string>("Ticker");
            var url = section.GetValue<string>("ExplorerUrl");

            var socials = _configuration.GetSection("Socials");
            var website = socials.GetValue<string>("Website");
            var twitter = socials.GetValue<string>("Twitter");
            var facebook = socials.GetValue<string>("Facebook");
            var telegram = socials.GetValue<string>("Telegram");
            var discord = socials.GetValue<string>("Discord");
            var reddit = socials.GetValue<string>("Reddit");
            var bitcointalk = socials.GetValue<string>("BitcoinTalk");
            var medium = socials.GetValue<string>("Medium");
            var github = socials.GetValue<string>("GitHub");

            ViewBag.Name = name;
            ViewBag.Ticker = ticker;
            ViewBag.Url = url;
            ViewBag.Website = website;
            ViewBag.Twitter = twitter;
            ViewBag.Facebook = facebook;
            ViewBag.Telegram = telegram;
            ViewBag.Discord = discord;
            ViewBag.Reddit = reddit;
            ViewBag.BitcoinTalk = bitcointalk;
            ViewBag.Medium = medium;
            ViewBag.GitHub = github;
            base.OnActionExecuting(filterContext);
        }
    }
}
