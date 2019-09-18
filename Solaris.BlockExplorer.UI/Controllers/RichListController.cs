using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Solaris.BlockExplorer.UI.Models;
using Solaris.BlockExplorer.UI.Models.ViewModels;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI.Controllers
{
    public class RichListController : BaseController
    {
        private readonly IRichListModelService _richListModelService;

        public RichListController(IRichListModelService richListModelService, IConfiguration configuration) : base(configuration)
        {
            _richListModelService = richListModelService;
        }

        public async Task<IActionResult> Index(PagingModel pagingModel)
        {
            if (pagingModel?.PageSize > 1000)
                pagingModel.PageSize = 1000;

            if (pagingModel?.PageNumber <= 0)
                pagingModel.PageSize = 0;

            var richList = await _richListModelService.GetRichList(pagingModel ?? new PagingModel());

            return View(new RichListViewModel
            {
                RichListItems = richList
            });
        }
    }
}