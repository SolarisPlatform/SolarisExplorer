using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solaris.BlockExplorer.UI.Services.Api;

namespace Solaris.BlockExplorer.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetDifficultyController : ControllerBase
    {
        private readonly IDifficultyModelService _difficultyModelService;

        public GetDifficultyController(IDifficultyModelService difficultyModelService)
        {
            _difficultyModelService = difficultyModelService;
        }

        public Task<decimal?> Index()
        {
            return _difficultyModelService.GetDifficulty();
        }
    }
}