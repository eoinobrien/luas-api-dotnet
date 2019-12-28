using System.Collections.Generic;
using System.Threading.Tasks;
using LuasAPI.NET.Infrastructure;
using LuasAPI.NET.Models;

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

		public StationForcast GetForcast(string stationAbbreviation)
		{
			IForcastClient client = new LuasForcastApiClient(stations);
			StationForcast forcast = client.GetRealTimeInfo(stationAbbreviation);

			return forcast;
		}

		public async Task<StationForcast> GetForcastAsync(string stationAbbreviation)
		{
			IForcastClient client = new LuasForcastApiClient(stations);
			StationForcast forcast = await client.GetRealTimeInfoAsync(stationAbbreviation);

			return forcast;
		}

		public StationForcast GetForcast(Station station)
		{
			return GetForcast(station.Abbreviation);
		}

		public async Task<StationForcast> GetForcastAsync(Station station)
		{
			return await GetForcastAsync(station.Abbreviation);
		}
	}
}