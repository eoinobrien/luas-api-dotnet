namespace LuasAPI.NET.Models
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using LuasAPI.NET.Models.RpaApiXml;

	public class StationForecast
	{
		public Station Station { get; private set; }

		public List<TramForecast> InboundTrams { get; private set; }

		public List<TramForecast> OutboundTrams { get; private set; }

		public string Message { get; private set; }

		public DateTime Created { get; private set; }

		public static StationForecast CreateStationForecastFromRealTimeInfo(RealTimeInfo realTimeInfo, Stations stations)
		{
			if (realTimeInfo == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument '{0}' is null.", nameof(realTimeInfo)));
			}
			if (stations == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument '{0}' is null.", nameof(stations)));
			}

			List<TramXml> inboundForecasts = realTimeInfo.Directions.FirstOrDefault(d => d.DirectionName == "Inbound").Trams;
			List<TramXml> outboundForecasts = realTimeInfo.Directions.FirstOrDefault(d => d.DirectionName == "Outbound").Trams;

			StationForecast stationForecast = new StationForecast
			{
				Station = stations.GetStation(realTimeInfo.StopAbbreviation),
				Message = realTimeInfo.Message,
				Created = realTimeInfo.Created,
				InboundTrams = inboundForecasts.Select(tram => TramForecast.CreateTramForecastFromTramXml(tram, stations)).Where(forecast => forecast != null).ToList(),
				OutboundTrams = outboundForecasts.Select(tram => TramForecast.CreateTramForecastFromTramXml(tram, stations)).Where(forecast => forecast != null).ToList()
			};

			return stationForecast;
		}
	}
}
