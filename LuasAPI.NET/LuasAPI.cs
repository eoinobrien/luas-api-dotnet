using System;
using System.Collections.Generic;
using LuasAPI.NET.Infrastructure;
using LuasAPI.NET.Models;
using Newtonsoft.Json;

namespace LuasAPI.NET
{
	public class LuasApi
	{
		private Stations stations;

		public LuasApi()
		{
			IStationInformationLoader stationInformationLoader = new StationInformationLoader();
			stations = new Stations(stationInformationLoader);
		}

		public List<Station> GetAllStations()
		{
			return stations.GetAllStations();
		}

		public Station GetStation(string abbreviation)
		{
			return stations.GetStation(abbreviation);
		}

		public StationForcast GetForcast(Station station)
		{
			LuasApiClient client = new LuasApiClient();
			StationForcast forcast = client.GetRealTimeInfo(station);

			Console.WriteLine(forcast.InboundTrams[0].Minutes);

			return forcast;
		}
	}
}