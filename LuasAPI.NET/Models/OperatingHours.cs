using System;
using System.Collections.Generic;
namespace LuasAPI.NET.Models
{
	[Serializable]
	public class OperatingHours
	{
		public OperatingHoursDay Weekdays { get; set; }
		public OperatingHoursDay Saturday { get; set; }
		public OperatingHoursDay Sunday { get; set; }
	}
}
