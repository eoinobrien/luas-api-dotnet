namespace LuasAPI.NET
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text.Json;
	using LuasAPI.NET.Models;

	public class StationInformationLoader : IStationInformationLoader
	{
		public Dictionary<string, Station> Load()
		{
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};

			var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), StationsDataFile));
			return JsonSerializer.Deserialize<Dictionary<string, Station>>(json, options);
		}

		private const string StationsDataFile = "StationInformation\\Stations.json";
	}
}
