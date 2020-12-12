using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuasAPI.NET.Infrastructure;
using LuasAPI.NET.Models;

namespace LuasAPI.NET.Tests.Infrastructure
{
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
