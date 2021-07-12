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
		/// <summary>
		///	City name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		public struct mainInfo
		{
			/// <summary>
			///	Temperature in C
			/// </summary>
			[JsonPropertyName("temp")]
			public double Temp { get; set; }
			/// <summary>
			///	Atmospheric pressure
			/// </summary>
			[JsonPropertyName("pressure")]
			public int Pressure { get; set; }
			/// <summary>
			///	Air humidity
			/// </summary>
			[JsonPropertyName("humidity")]
			public int Humidity { get; set; }
		}

		public struct wind
		{
			/// <summary>
			///	Wind speed
			/// </summary>
			[JsonPropertyName("speed")]
			public double Speed { get; set; }
		}

		public struct weather
		{
			/// <summary>
			/// Weather description
			/// </summary>
			[JsonPropertyName("description")]
			public string Description { get; set; }
		}
    }
}
