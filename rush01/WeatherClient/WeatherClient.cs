using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using rush01.WeatherClient.Models;

namespace rush01.WeatherClient
{
	public class WeatherClient
	{
		private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?";
		private readonly ServiceSettings _options;

		public WeatherClient(IOptions<ServiceSettings> options)
		{
			_options = options.Value;
		}
		
		public async Task<WeatherForecast> GetAsync(double latitude, double longitude)
		{
			var url = $"{ApiUrl}lat={latitude}&lon={longitude}&appid={_options.ApiKey}";
			
			return await HttpGetAsync<WeatherForecast>(url);
		}

		public async Task<WeatherForecast> GetAsync(string cityName)
		{
			var url = $"{ApiUrl}q={cityName}&appid={_options.ApiKey}";
			
			return await HttpGetAsync<WeatherForecast>(url);
		}

		private async Task<T> HttpGetAsync<T>(string url)
		{
			var client = new HttpClient();
			var response = await client.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<T>(content);
		}
	}
}
