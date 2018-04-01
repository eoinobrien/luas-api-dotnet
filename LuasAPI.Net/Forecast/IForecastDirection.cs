using System;
using System.Collections.Generic;
using System.Text;

namespace LuasAPI.NET.Forecast
{
	public interface IForecastDirection
	{
		List<Tram> Trams { get; set; }

		string DirectionName { get; set; }

		Direction Direction { get; }

		bool NoTramsForcast { get; }
	}
}
