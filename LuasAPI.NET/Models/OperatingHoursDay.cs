using System;

namespace LuasAPI.NET.Models
{
	[Serializable]
	public class OperatingHoursDay
	{
		public OperatingHoursDirection Inbound { get; set; }
		public OperatingHoursDirection Outbound { get; set; }
	}
}
