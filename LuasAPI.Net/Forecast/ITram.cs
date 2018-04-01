using LuasAPI.NET.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuasAPI.NET.Forecast
{
	public interface ITram
	{
		string DueMins { get; set; }

		string Destination { get; set; }

		IStation DestinationStation { get; }

		bool IsDue { get; }

		bool NoTramsForcast { get; }

		int Minutes { get; }
	}
}
