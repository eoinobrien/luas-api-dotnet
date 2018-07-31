using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LuasAPI.NET
{
	public static class Stations
	{
		static Stations()
		{
			try
			{
				using (Stream fileStream = typeof(Station).Assembly.GetManifestResourceStream(stationsDataFile))
				using (StreamReader file = new StreamReader(fileStream))
				{
					JsonSerializer serializer = new JsonSerializer();
					StationsList = (List<Station>)serializer.Deserialize(file, typeof(List<Station>));
					Loaded = true;
				}
			}
			catch (Exception e)
			{
				// TODO
			}

			StationsList.ForEach(station => station.ConvertStringsToStations());
		}

		public static List<Station> StationsList { get; private set; }

		public static readonly bool Loaded;

		public static Station GetFromAbbreviation(string abbreviation)
		{
			if (string.IsNullOrWhiteSpace(abbreviation))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(abbreviation), CultureInfo.InvariantCulture));
			}

			if (!Loaded)
			{
				throw new InvalidOperationException("Stations have not been loaded successfully.");
			}

			return StationsList.FirstOrDefault(
				st => st.Abbreviation.ToUpperInvariant() == abbreviation.ToUpperInvariant());
		}

		public static Station GetFromName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(name), CultureInfo.InvariantCulture));
			}

			if (!Loaded)
			{
				throw new InvalidOperationException("Stations have not been loaded successfully.");
			}

			return StationsList.FirstOrDefault(
				st => st.Name.ToUpperInvariant() == name.ToUpperInvariant());
		}

		public static Station GetFromNameOrAbbreviation(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(input), CultureInfo.InvariantCulture));
			}

			if (!Loaded)
			{
				throw new InvalidOperationException("Stations have not been loaded successfully.");
			}

			return StationsList.FirstOrDefault(
				st => st.Name.ToUpperInvariant() == input.ToUpperInvariant() ||
				st.Abbreviation.ToUpperInvariant() == input.ToUpperInvariant());
		}

		private const string stationsDataFile = "LuasAPI.NET.StationsData.json";
	}
}
