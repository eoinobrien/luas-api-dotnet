namespace LuasAPI.NET
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using LuasAPI.NET.Models;
	using Newtonsoft.Json;

	class StationInformationLoader : IStationInformationLoader
	{
		public Dictionary<string, Station> Load()
		{
			using (Stream fileStream = typeof(Station).Assembly.GetManifestResourceStream(stationsDataFile))
			using (StreamReader file = new StreamReader(fileStream))
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				return (Dictionary<string, Station>)jsonSerializer.Deserialize(file, typeof(Dictionary<string, Station>));
			}
		}

		private const string stationsDataFile = "LuasAPI.NET.StationInformation.Stations.json";
	}
}
