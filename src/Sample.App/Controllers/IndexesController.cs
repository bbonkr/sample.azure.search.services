
using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.AspNetCore.Filters;

using Microsoft.AspNetCore.Mvc;
using Sample.Services;
using System.Threading.Tasks;

namespace Sample.App.Controllers
{
    [ApiController]
    [ApiExceptionHandlerFilter]
    [Area(DefaultValues.AreaName)]
    [Route(DefaultValues.RouteTemplate)]
    [ApiVersion(DefaultValues.ApiVersion)]
    public class IndexesController : ApiControllerBase
    {
        public IndexesController(ISearchIndexService searchIndexService)
        {
            this.searchIndexService = searchIndexService;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var result = await searchIndexService.CreateIndex();

            return StatusCode(System.Net.HttpStatusCode.OK, result);
        }

        private readonly ISearchIndexService searchIndexService;
    }
}
