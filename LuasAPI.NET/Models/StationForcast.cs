using LuasAPI.NET.Models.RpaApiXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LuasAPI.NET.Models
{
	public class StationForcast
	{
		public Station Station { get; private set; }

		public List<TramForcast> InboundTrams { get; private set; }

		public List<TramForcast> OutboundTrams { get; private set; }

		public string Message { get; private set; }

		public DateTime Created { get; private set; }

		public static StationForcast CreateStationForcastFromRealTimeInfo(RealTimeInfo realTimeInfo, Stations stations)
		{
			if (realTimeInfo == null)
			{
				throw new ArgumentException(string.Format("Argument '{0}' is null.", nameof(realTimeInfo), CultureInfo.InvariantCulture));
			}
			if (stations == null)
			{
				throw new ArgumentException(string.Format("Argument '{0}' is null.", nameof(stations), CultureInfo.InvariantCulture));
			}

			List<TramXml> inboundForcasts = realTimeInfo.Directions.FirstOrDefault(d => d.DirectionName == "Inbound").Trams;
			List<TramXml> outboundForcasts = realTimeInfo.Directions.FirstOrDefault(d => d.DirectionName == "Outbound").Trams;

			StationForcast stationForcast = new StationForcast
			{
				Station = stations.GetStation(realTimeInfo.StopAbbreviation),
				Message = realTimeInfo.Message,
				Created = realTimeInfo.Created,
				InboundTrams = inboundForcasts.Select(tram => TramForcast.CreateTramForcastFromTramXml(tram, stations)).Where(forcast => forcast != null).ToList(),
				OutboundTrams = outboundForcasts.Select(tram => TramForcast.CreateTramForcastFromTramXml(tram, stations)).Where(forcast => forcast != null).ToList()
			};

			return stationForcast;
		}
	}
}
