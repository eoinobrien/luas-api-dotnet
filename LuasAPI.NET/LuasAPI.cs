using System.Collections.Generic;
using System.Threading.Tasks;
using LuasAPI.NET.Infrastructure;
using LuasAPI.NET.Models;

namespace LuasAPI.NET
{
	public class LuasApi
	{
		private readonly Stations stations;

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

		public StationForecast GetForecast(string stationAbbreviation)
		{
			return GetForecastAsync(stationAbbreviation).Result;
		}

		public async Task<StationForecast> GetForecastAsync(string stationAbbreviation)
		{
			Station station = GetStation(stationAbbreviation);

			IForecastClient client = new LuasForecastApiClient(stations);
			StationForecast forecast = await client.GetRealTimeInfoAsync(station.Abbreviation);

			return forecast;
		}

		public StationForecast GetForecast(Station station)
		{
			return GetForecast(station.Abbreviation);
		}

		public async Task<StationForecast> GetForecastAsync(Station station)
		{
			return await GetForecastAsync(station.Abbreviation);
		}
	}
}