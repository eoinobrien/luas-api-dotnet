namespace LuasAPI.NET.Models
{
	using System;
	using JsonTools;
	using Newtonsoft.Json;

	[Serializable]
	public class OperatingHoursDirection
	{
		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan FirstTram { get; set; }

		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan LastTram { get; set; }
	}
}
