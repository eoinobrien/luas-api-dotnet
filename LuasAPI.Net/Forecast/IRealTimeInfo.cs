using System;
using System.Collections.Generic;

namespace LuasAPI.NET.Forecast
{
	public interface IRealTimeInfo
	{
		string Message { get; set; }

		bool ServicesOperatingNormally { get; }

		List<ForecastDirection> Directions { get; set; }

		DateTime Created { get; set; }

		string Stop { get; set; }

		string StopAbbreviation { get; set; }
	}
}
