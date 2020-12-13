namespace LuasAPI.NET.Models
{
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class OperatingHours
	{
		public OperatingHoursDay Weekdays { get; set; }
		public OperatingHoursDay Saturday { get; set; }
		public OperatingHoursDay Sunday { get; set; }
	}
}
