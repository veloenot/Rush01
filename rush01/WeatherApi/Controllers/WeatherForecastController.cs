using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rush01.WeatherClient;
using rush01.WeatherClient.Models;


namespace rush01.WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
		private readonly WeatherClient.WeatherClient _weatherClient;

		public WeatherForecastController(IOptions<ServiceSettings> options)
		{
			_weatherClient = new WeatherClient.WeatherClient(options);
		}		

        /// <summary>
		/// Generates weather forecast by coordinates.
        /// </summary>
		[HttpGet]
		[Route("coord")]	
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery]double lat, [FromQuery]double lon)
		{
			try
			{
				return Ok(await _weatherClient.GetAsync(lat, lon));
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
			try
			{
				return Ok(await _weatherClient.GetAsync(cityName));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
    }
}
