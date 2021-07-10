using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace rush01
{
	public class WeatherClient
	{
		private string ApiUrl { get; set; }
		private string ApiKey { get; set; }

		public WeatherClient()
		{
			ApiUrl = "http://api.openweathermap.org/data/2.5/weather?";
			ApiKey = "44d2e7921e2da1450e99c6cb47d08d0a";
		}

		public async Task<WeatherForecast> GetAsync(double latitude, double longitude)
		{
			var url = $"{ApiUrl}lat={latitude}&lon={longitude}&appid={ApiKey}";
			
			return await HttpGetAsync<WeatherForecast>(url);
		}

		public async Task<WeatherForecast> GetAsync(string cityName)
		{
			var url = $"{ApiUrl}q={cityName}&appid={ApiKey}";
			
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
