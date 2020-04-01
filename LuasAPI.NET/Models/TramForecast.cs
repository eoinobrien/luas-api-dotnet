using LuasAPI.NET.Models.RpaApiXml;
using System;

namespace LuasAPI.NET.Models
{
	public class TramForecast
	{
		public Station DestinationStation { get; private set; }

		public bool IsDue { get; private set; }

		public int Minutes { get; private set; }


		public static TramForecast CreateTramForecastFromTramXml(TramXml tramXml, Stations stations)
		{
			if (tramXml?.Destination == "No trams forecast" && string.IsNullOrEmpty(tramXml.Destination))
			{
				return null;
			}

			if (stations == null)
			{
				throw new ArgumentNullException(nameof(stations));
			}

			if (!int.TryParse(tramXml.DueMins, out int dueMinutes))
			{
				dueMinutes = 0;
			}

			try
			{
				Station destinationStation = stations.GetStationFromName(tramXml.Destination);

				TramForecast tramForecast = new TramForecast
				{
					DestinationStation = destinationStation,
					Minutes = dueMinutes,
					IsDue = dueMinutes == 0
				};

				return tramForecast;
			}
			catch (StationNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return null;
		}
	}
}
