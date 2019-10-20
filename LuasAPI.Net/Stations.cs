﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LuasAPI.NET.Models;

namespace LuasAPI.NET
{
	public class Stations
	{
		public Stations(IStationInformationLoader stationInformationLoader)
		{
			stations = stationInformationLoader.Load();
		}

		private Dictionary<string, Station> stations { get; set; }

		public List<Station> GetAllStations()
		{
			return stations.Values.ToList();
		}

		public Station GetStation(string abbreviation)
		{
			if (string.IsNullOrWhiteSpace(abbreviation))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(abbreviation), CultureInfo.InvariantCulture));
			}

			Station station;
			stations.TryGetValue(abbreviation.ToUpperInvariant(), out station);

			if(station == null)
			{
				throw new StationNotFoundException(string.Format("Station Abbreviation '{0}' was not found in list of stations", abbreviation, CultureInfo.InvariantCulture));
			}

			return station;
		}

		public Station GetStationFromName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(name), CultureInfo.InvariantCulture));
			}

			string uppercaseName = name.ToUpperInvariant();
			Station station = stations.Values.FirstOrDefault(s => s.Name.ToUpperInvariant() == uppercaseName);

			if (station == null)
			{
				throw new StationNotFoundException(string.Format("Station Name '{0}' was not found in list of stations", name, CultureInfo.InvariantCulture));
			}

			return station;
		}

		//public static Station GetFromName(string name)
		//{
		//	if (string.IsNullOrWhiteSpace(name))
		//	{
		//		throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(name), CultureInfo.InvariantCulture));
		//	}

		//	if (!Loaded)
		//	{
		//		throw new InvalidOperationException("Stations have not been loaded successfully.");
		//	}

		//	return StationsList.FirstOrDefault(
		//		st => st.Name.ToUpperInvariant() == name.ToUpperInvariant());
		//}

		//public static Station GetFromNameOrAbbreviation(string input)
		//{
		//	if (string.IsNullOrWhiteSpace(input))
		//	{
		//		throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(input), CultureInfo.InvariantCulture));
		//	}

		//	if (!Loaded)
		//	{
		//		throw new InvalidOperationException("Stations have not been loaded successfully.");
		//	}

		//	return StationsList.FirstOrDefault(
		//		st => st.Name.ToUpperInvariant() == input.ToUpperInvariant() ||
		//		st.Abbreviation.ToUpperInvariant() == input.ToUpperInvariant());
		//}
	}
}
