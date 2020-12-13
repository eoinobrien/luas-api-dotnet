namespace LuasAPI.NET.Models
{
	using System;

	[Serializable]
	public class OperatingHoursDay
	{
		public OperatingHoursDirection Inbound { get; set; }
		public OperatingHoursDirection Outbound { get; set; }
	}
}
