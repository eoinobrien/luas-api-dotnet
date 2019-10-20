using System.Collections.Generic;
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

		public string GetAllStations()
		{
			return JsonConvert.SerializeObject(stations.GetAllStations());
		}

		public string GetStation(string abbreviation)
		{
			return JsonConvert.SerializeObject(stations.GetStation(abbreviation));
		}
	}
}