using LuasAPI.NET.Models;
using System.Collections.Generic;

namespace LuasAPI.NET
{
	public interface IStationInformationLoader
	{
		Dictionary<string, Station> Load();
	}
}
