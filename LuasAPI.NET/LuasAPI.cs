namespace LuasAPI.NET
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using LuasAPI.NET.Infrastructure;
	using LuasAPI.NET.Models;

	public class LuasApi
	{
		private readonly Stations stations;

		public LuasApi()
		{
			IStationInformationLoader stationInformationLoader = new StationInformationLoader();
			this.stations = new Stations(stationInformationLoader);
		}

		public List<Station> GetAllStations()
		{
			return this.stations.GetAllStations();
		}

		public Station GetStation(string abbreviation)
		{
			return this.stations.GetStation(abbreviation);
		}

		public StationForecast GetForecast(string stationAbbreviation)
		{
			return GetForecastAsync(stationAbbreviation).Result;
		}

		public async Task<StationForecast> GetForecastAsync(string stationAbbreviation)
		{
			IForecastClient client = new LuasForecastApiClient(this.stations);
			StationForecast forecast = await client.GetRealTimeInfoAsync(stationAbbreviation).ConfigureAwait(false);

			return forecast;
		}

		public StationForecast GetForecast(Station station)
		{
			if (station == null)
			{
				throw new ArgumentNullException(nameof(station), "Cannot get a forecast for a station that does not exist.");
			}

			if (string.IsNullOrEmpty(station.Abbreviation))
			{
				throw new ArgumentNullException(nameof(station), "Cannot get a forecast for a station that does not exist.");
			}

			return GetForecast(station.Abbreviation);
		}

		public async Task<StationForecast> GetForecastAsync(Station station)
		{
			if (station == null)
			{
				throw new ArgumentNullException(nameof(station), "Cannot get a forecast for a station that does not exist.");
			}

			if (string.IsNullOrEmpty(station.Abbreviation))
			{
				throw new ArgumentNullException(nameof(station), "Cannot get a forecast for a station that does not exist.");
			}

			return await GetForecastAsync(station.Abbreviation).ConfigureAwait(false);
		}
	}
}
