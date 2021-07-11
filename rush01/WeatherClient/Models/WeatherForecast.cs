using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rush01.WeatherClient.Models
{
    public class WeatherForecast
    {
		[JsonPropertyName("wind")]
		public wind Wind { get; set; }
		[JsonPropertyName("weather")]
		public List<weather> Weather { get; set; }
		[JsonPropertyName("main")]
		public mainInfo MainInfo { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }

		public struct mainInfo
		{
			private double temp;
			[JsonPropertyName("temp")]
			public double Temp
			{
				get => Math.Round(temp, 2); 
				set => temp = value - 273; 
			}
			[JsonPropertyName("pressure")]
			public int Pressure { get; set; }
			[JsonPropertyName("humidity")]
			public int Humidity { get; set; }
		}

		public struct wind
		{
			[JsonPropertyName("speed")]
			public double Speed { get; set; }
		}

		public struct weather
		{
			[JsonPropertyName("description")]
			public string Description { get; set; }
		}
    }
}
