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

            ViewBag.Name = name;
            ViewBag.Ticker = ticker;
            base.OnActionExecuting(filterContext);
        }
    }
}
