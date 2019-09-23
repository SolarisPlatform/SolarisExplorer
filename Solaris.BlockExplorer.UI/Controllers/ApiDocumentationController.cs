using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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