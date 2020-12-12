using System.Threading.Tasks;
using LuasAPI.NET.Models;

namespace LuasAPI.NET.Infrastructure
{
	public interface IForecastClient
	{
		StationForecast GetRealTimeInfo(string stationAbbreviation);
		Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation);
	}
}
