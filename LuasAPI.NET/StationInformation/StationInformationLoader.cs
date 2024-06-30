namespace LuasAPI.NET
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text.Json;
	using LuasAPI.NET.Models;

	public class StationInformationLoader : IStationInformationLoader
	{
		private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public Dictionary<string, Station> Load()
		{

			var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "StationInformation", "Stations.json"));
			return JsonSerializer.Deserialize<Dictionary<string, Station>>(json, this.jsonSerializerOptions);
		}
	}
}
