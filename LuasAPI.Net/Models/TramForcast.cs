using LuasAPI.NET.Models.RpaApiXml;
using System;

namespace LuasAPI.NET.Models
{
	public class TramForcast
	{
		public string DestinationStationName { get; private set; }

		public bool IsDue { get; private set; }

		public int Minutes { get; private set; }

		
		public static explicit operator TramForcast(TramXml tramXml)
		{
			if (tramXml.Destination == "No trams forecast" && tramXml.Destination == string.Empty)
			{
				return null;
			}

			int dueMinutes;
			if (!int.TryParse(tramXml.DueMins, out dueMinutes))
			{
				dueMinutes = 0;
			}

			TramForcast tramForcast = new TramForcast
			{
				DestinationStationName = tramXml.Destination,
				Minutes = dueMinutes,
				IsDue = dueMinutes == 0
			};

			return tramForcast;
		}
	}
}
