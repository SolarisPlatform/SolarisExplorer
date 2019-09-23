using Microsoft.AspNetCore.Mvc;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class ApiDocumentationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}