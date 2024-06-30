namespace LuasAPI.NET.Models
{
	using System;

	[Serializable]
	public class OperatingHours
	{
		public OperatingHoursDay Weekdays { get; set; }
		public OperatingHoursDay Saturday { get; set; }
		public OperatingHoursDay Sunday { get; set; }
	}
}
