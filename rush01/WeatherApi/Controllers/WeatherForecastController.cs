using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using rush01.WeatherClient;
using rush01.WeatherClient.Models;


namespace rush01.WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
		private readonly WeatherClient.WeatherClient _weatherClient;
		private IMemoryCache _cache;

		public WeatherForecastController(IOptions<ServiceSettings> options, IMemoryCache memoryCache)
		{
			_weatherClient = new WeatherClient.WeatherClient(options);
			_cache = memoryCache;
		}		

        /// <summary>
		/// Gets weather forecast by coordinates.
        /// </summary>
		[HttpGet]
		[Route("coord")]	
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
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
		/// Gets weather forecast by city name.
        /// </summary>
		[HttpGet]
		[Route("{cityName?}")]	
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(string cityName = null)
		{
			if (string.IsNullOrWhiteSpace(cityName) && !_cache.TryGetValue("default city name", out cityName))
				return NotFound("No city name was entered.");
			try
			{
				return Ok(await _weatherClient.GetAsync(cityName));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Sets default city name.
		/// </summary>
		[HttpPost]
		[Route("{cityName?}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult Post(string cityName = null)
		{
			if (string.IsNullOrWhiteSpace(cityName))
				return NotFound("No city name was entered.");

			_cache.Set("default city name", cityName);
			return Ok($"{cityName} set as default city name");
		}
    }
}
