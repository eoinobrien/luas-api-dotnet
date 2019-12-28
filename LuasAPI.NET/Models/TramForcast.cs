using LuasAPI.NET.Models.RpaApiXml;
using System;

namespace LuasAPI.NET.Models
{
	public class TramForcast
	{
		public Station DestinationStation { get; private set; }

		public bool IsDue { get; private set; }

		public int Minutes { get; private set; }

		
		public static TramForcast CreateTramForcastFromRTramXml(TramXml tramXml, Stations stations)
		{
			if (tramXml.Destination == "No trams forecast" && tramXml.Destination == string.Empty)
			{
				return null;
			}

			if (!int.TryParse(tramXml.DueMins, out int dueMinutes))
			{
				dueMinutes = 0;
			}

			try
			{
				Station destinationStation = stations.GetStationFromName(tramXml.Destination);

				TramForcast tramForcast = new TramForcast
				{
					DestinationStation = destinationStation,
					Minutes = dueMinutes,
					IsDue = dueMinutes == 0
				};

				return tramForcast;
			}
			catch (StationNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return null;
		}
	}
}
