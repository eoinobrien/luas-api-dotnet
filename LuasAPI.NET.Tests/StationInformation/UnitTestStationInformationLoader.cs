using System.Collections.Generic;
using LuasAPI.NET.Models;

namespace LuasAPI.NET.Tests.StationInformation
{
	class UnitTestStationInformationLoader : IStationInformationLoader
	{
		public Dictionary<string, Station> stations { get; set; } = new Dictionary<string, Station>();

		public Dictionary<string, Station> Load()
		{
			return stations;
		}

		public void AddStations(params Station[] stationsToAdd)
		{
			foreach (Station station in stationsToAdd)
			{
				stations.Add(station.Abbreviation, station);
			}
		}
	}
}
