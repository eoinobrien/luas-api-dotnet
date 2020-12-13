namespace LuasAPI.NET
{
	using System.Collections.Generic;
	using LuasAPI.NET.Models;

	public interface IStationInformationLoader
	{
		Dictionary<string, Station> Load();
	}
}
