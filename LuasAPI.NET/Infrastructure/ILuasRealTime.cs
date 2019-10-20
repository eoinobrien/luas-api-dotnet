using LuasAPI.NET.Models;

namespace LuasAPI.NET.Infrastructure
{
	public interface ILuasRealTime
	{
		StationForcast GetRealTimeInfo(Station station);
	}
}