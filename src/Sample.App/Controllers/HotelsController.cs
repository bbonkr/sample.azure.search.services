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
        public IActionResult GetHotels(string keyword = "")
        {
            var result = searchService.SearchByHotelName(keyword);

            return StatusCode(HttpStatusCode.OK, result);
        }

        private readonly ISearchService searchService;
    }
}
