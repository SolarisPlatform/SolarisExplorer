using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class ApiDocumentationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ApiDocumentationController(IConfiguration configuration) : base(configuration)
        {
        }
    }
}