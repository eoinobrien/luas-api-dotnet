using System.Collections.Generic;
using LuasAPI.NET.Models;

namespace LuasAPI.NET
{
	public interface IStationInformationLoader
	{
		Dictionary<string, Station> Load();
	}
}
