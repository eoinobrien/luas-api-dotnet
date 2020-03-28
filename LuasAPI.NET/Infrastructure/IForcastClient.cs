using LuasAPI.NET.Models;
using System.Threading.Tasks;

namespace LuasAPI.NET.Infrastructure
{
	public interface IForecastClient
	{
		StationForecast GetRealTimeInfo(string stationAbbreviation);
		Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation);
	}
}