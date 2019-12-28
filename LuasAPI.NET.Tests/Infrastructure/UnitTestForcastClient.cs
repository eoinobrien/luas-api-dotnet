using LuasAPI.NET.Infrastructure;
using LuasAPI.NET.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuasAPI.NET.Tests.Infrastructure
{
	public class UnitTestForcastClient : IForcastClient
	{
		public StationForcast GetRealTimeInfo(string stationAbbreviation)
		{
			throw new NotImplementedException();
		}

		public Task<StationForcast> GetRealTimeInfoAsync(string stationAbbreviation)
		{
			throw new NotImplementedException();
		}
	}
}
