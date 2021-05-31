using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using kr.bbon.AspNetCore;
using kr.bbon.AspNetCore.Mvc;
using kr.bbon.AspNetCore.Filters;

using Microsoft.AspNetCore.Mvc;
using Sample.Services;
using System.Net;
using System.Threading;
using Sample.Services.Models;

namespace Sample.App.Controllers
{
    [ApiController]
    [ApiExceptionHandlerFilter]
    [Area(DefaultValues.AreaName)]
    [Route(DefaultValues.RouteTemplate)]
    [ApiVersion(DefaultValues.ApiVersion)]
    public class HotelsController : ApiControllerBase
    {
        public HotelsController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<HotelModel>))]
        public async Task<IActionResult> GetHotels(string keyword = "", CancellationToken cancellationToken = default)
        {
            var result = await searchService.SearchByHotelNameAsync(keyword, cancellationToken);

            return StatusCode(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public async Task< IActionResult> AddDocuments([FromBody] IEnumerable<HotelModel> models, CancellationToken cancellationToken = default)
        {
            var result = await searchService.UploadAsync(models, cancellationToken);

            return StatusCode(HttpStatusCode.Accepted, result);
        }

        [HttpPatch]
        public async Task< IActionResult> MergeDocuments([FromBody] IEnumerable<HotelModel> models, CancellationToken cancellationToken = default)
        {
             await searchService.MergeAsync(models, cancellationToken);

            return StatusCode(HttpStatusCode.Accepted);
        }

        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteDocument([FromRoute] string hotelId, CancellationToken cancellationToken = default)
        {
            await searchService.DeleteAsync(new List<HotelModel> {
                new HotelModel
                {
                    HotelId = hotelId,
                },
            }, cancellationToken);

            return StatusCode(HttpStatusCode.OK);
        }

        private readonly ISearchService searchService;
    }
}
