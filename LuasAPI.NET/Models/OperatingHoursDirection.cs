namespace LuasAPI.NET.Models
{
	using System;
	using System.Text.Json.Serialization;

	[Serializable]
	public class OperatingHoursDirection
	{
		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan FirstTram { get; set; }

		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan LastTram { get; set; }
	}
}
