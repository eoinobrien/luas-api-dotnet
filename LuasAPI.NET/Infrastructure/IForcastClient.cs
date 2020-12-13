namespace LuasAPI.NET.Infrastructure
{
	using System.Threading.Tasks;
	using LuasAPI.NET.Models;

	public interface IForecastClient
	{
		StationForecast GetRealTimeInfo(string stationAbbreviation);
		Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation);
	}
}
