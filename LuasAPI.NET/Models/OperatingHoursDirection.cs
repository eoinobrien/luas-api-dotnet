using JsonTools;
using Newtonsoft.Json;
using System;

namespace LuasAPI.NET.Models
{
	[Serializable]
	public class OperatingHoursDirection
	{
		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan FirstTram { get; set; }

		[JsonConverter(typeof(TimeConverter))]
		public TimeSpan LastTram { get; set; }
	}
}
