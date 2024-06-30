namespace LuasAPI.NET
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using LuasAPI.NET.Models;

	public class Stations
	{
		public Stations(IStationInformationLoader stationInformationLoader)
		{
			if (stationInformationLoader == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument '{0}' is null.", nameof(stationInformationLoader)));
			}

			this.StationsDictionary = stationInformationLoader.Load();
		}

		private Dictionary<string, Station> StationsDictionary { get; set; }

		public List<Station> GetAllStations(bool returnOnlyStationsInUse = true)
		{
			return this.StationsDictionary.Values.Where(s => s.IsInUse || !returnOnlyStationsInUse).ToList();
		}

		public Station GetStation(string abbreviation)
		{
			if (string.IsNullOrWhiteSpace(abbreviation))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument '{0}' is either null or whitespace.", nameof(abbreviation)), nameof(abbreviation));
			}

			this.StationsDictionary.TryGetValue(abbreviation.ToUpperInvariant(), out Station station);

			if (station == null)
			{
				throw new StationNotFoundException(abbreviation, string.Format(CultureInfo.InvariantCulture, "Station Abbreviation '{0}' was not found in list of stations", abbreviation));
			}

			return station;
		}

		public Station GetStationFromName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument '{0}' is either null or whitespace.", nameof(name)));
			}

			string uppercaseName = name.ToUpperInvariant();
			Station station = this.StationsDictionary.Values.FirstOrDefault(s => s.Name?.ToUpperInvariant() == uppercaseName);

			if (station == null)
			{
				throw new StationNotFoundException(name, string.Format(CultureInfo.InvariantCulture, "Station Name '{0}' was not found in list of stations", name));
			}

			return station;
		}
	}
}
