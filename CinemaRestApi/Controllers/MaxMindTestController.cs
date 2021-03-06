using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaxMindTestController : ControllerBase
    {
        private readonly WebServiceClient _maxMindClient;
        public MaxMindTestController(WebServiceClient maxmind)
        {
            _maxMindClient = maxmind;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var location = await _maxMindClient.CountryAsync(id);

            return Ok(location);
        }
    }
}
