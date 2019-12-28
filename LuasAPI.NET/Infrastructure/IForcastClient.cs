using LuasAPI.NET.Models;
using System.Threading.Tasks;

namespace LuasAPI.NET.Infrastructure
{
	public interface IForcastClient
	{
		StationForcast GetRealTimeInfo(string stationAbbreviation);
		Task<StationForcast> GetRealTimeInfoAsync(string stationAbbreviation);
	}
}