using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using rush01;

namespace rush01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
		/// Generates weather forecast by coordinates.
        /// </summary>
		[HttpGet]
		[Route("coord")]	
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery]double lat, [FromQuery]double lon)
		{
			var weatherClient = new WeatherClient();

			try
			{
				return Ok(await weatherClient.GetAsync(lat, lon));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        /// <summary>
		/// Generates weather forecast by city name.
        /// </summary>
		[HttpGet]
		[Route("{cityName}")]	
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(string cityName)
		{
			var weatherClient = new WeatherClient();

			try
			{
				return Ok(await weatherClient.GetAsync(cityName));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
    }
}
