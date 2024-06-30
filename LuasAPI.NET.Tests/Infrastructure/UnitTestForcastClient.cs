namespace LuasAPI.NET.Tests.Infrastructure
{
	using System;
	using System.Threading.Tasks;
	using LuasAPI.NET.Infrastructure;
	using LuasAPI.NET.Models;

	public class UnitTestForecastClient : IForecastClient
	{
		public StationForecast GetRealTimeInfo(string stationAbbreviation)
		{
			throw new NotImplementedException();
		}

		public Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation)
		{
			throw new NotImplementedException();
		}
	}
}
