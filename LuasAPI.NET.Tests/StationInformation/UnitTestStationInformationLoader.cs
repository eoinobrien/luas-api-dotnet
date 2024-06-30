namespace LuasAPI.NET.Tests.StationInformation
{
	using System.Collections.Generic;
	using LuasAPI.NET.Models;

	public class UnitTestStationInformationLoader : IStationInformationLoader
	{
		public Dictionary<string, Station> stations { get; set; } = new Dictionary<string, Station>();

		public Dictionary<string, Station> Load()
		{
			return this.stations;
		}

		public void AddStations(params Station[] stationsToAdd)
		{
			foreach (Station station in stationsToAdd)
			{
				this.stations.Add(station.Abbreviation, station);
			}
		}
	}
}
