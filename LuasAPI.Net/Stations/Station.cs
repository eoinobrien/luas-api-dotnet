using LuasAPI.NET.Forecast;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace LuasAPI.NET.Stations
{
	public class Station
	{
		static Station()
		{
			LoadStations();
		}

		public Station(string station, string pronunciation, string abbreviation, Line line)
		{
			Name = station;
			Pronunciation = pronunciation;
			Abbreviation = abbreviation.ToUpperInvariant();
			Line = line;
		}

		[JsonProperty(PropertyName = "name")]
		public string Name { get; private set; }

		[JsonProperty(PropertyName = "pronunciation")]
		public string Pronunciation { get; private set; }

		[JsonProperty(PropertyName = "abbreviation")]
		public string Abbreviation { get; private set; }

		[JsonProperty(PropertyName = "line")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Line Line { get; private set; }

		public List<Station> InboundStations { get; private set; }

		public List<Station> OutboundStations { get; private set; }

		public List<Station> WalkingTransfer { get; private set; }

		[JsonProperty(PropertyName = "in-use")]
		public bool IsInUse { get; private set; }


		[JsonProperty(PropertyName = "inbound-stations")]
		private List<string> inboundStations { get; set; }

		[JsonProperty(PropertyName = "outbound-stations")]
		private List<string> outboundStations { get; set; }

		[JsonProperty(PropertyName = "walking-transfer")]
		private List<string> walkingTransfer { get; set; }

		private static bool stationsLoaded;


		public void ConvertStringsToStations()
		{
			InboundStations = new List<Station>();
			OutboundStations = new List<Station>();
			WalkingTransfer = new List<Station>();

			inboundStations.ForEach(st => InboundStations.Add(Station.GetFromAbbreviation(st)));
			outboundStations.ForEach(st => OutboundStations.Add(Station.GetFromAbbreviation(st)));
			walkingTransfer.ForEach(st => WalkingTransfer.Add(Station.GetFromAbbreviation(st)));
		}


		public List<Station> GetDirectionStations(Direction direction)
		{
			if (!stationsLoaded)
			{
				throw new InvalidOperationException(string.Format("Stations have not been loaded successfully.", CultureInfo.InvariantCulture));
			}

			if (direction == Direction.Inbound)
			{
				return InboundStations;
			}
			else if (direction == Direction.Outbound)
			{
				return OutboundStations;
			}

			return new List<Station>();
		}


		public static List<Station> Stations { get; private set; }


		public static Station GetFromAbbreviation(string abbreviation)
		{
			if (string.IsNullOrWhiteSpace(abbreviation))
			{
				throw new ArgumentException(string.Format("Argument '{0}' is either null or whitespace.", nameof(abbreviation), CultureInfo.InvariantCulture));
			}

			if (!stationsLoaded)
			{
				throw new InvalidOperationException(string.Format("Stations have not been loaded successfully.", CultureInfo.InvariantCulture));
			}

			return Stations.FirstOrDefault(st => st.Abbreviation.ToUpperInvariant() == abbreviation.ToUpperInvariant());
		}


		public static Station GetFromName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}

			if (!stationsLoaded)
			{
				throw new InvalidOperationException(string.Format("Stations have not been loaded successfully.", CultureInfo.InvariantCulture));
			}

			return Stations.FirstOrDefault(st => st.Name.ToUpperInvariant() == name.ToUpperInvariant());
		}


		public static Station GetFromNameOrAbbreviation(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return null;
			}

			if (!stationsLoaded)
			{
				throw new InvalidOperationException(string.Format("Stations have not been loaded successfully.", CultureInfo.InvariantCulture));
			}

			return Stations.FirstOrDefault(st => st.Name.ToUpperInvariant() == input.ToUpperInvariant() || st.Abbreviation.ToUpperInvariant() == input.ToUpperInvariant());
		}


		private const string stationsDataFile = "LuasAPI.NET.Stations.StationsData.json";


		private static bool LoadStations()
		{
			try
			{
				using (Stream fileStream = typeof(Station).Assembly.GetManifestResourceStream(stationsDataFile))
				using (StreamReader file = new StreamReader(fileStream))
				{
					JsonSerializer serializer = new JsonSerializer();
					Stations = (List<Station>)serializer.Deserialize(file, typeof(List<Station>));
					stationsLoaded = true;
				}
			}
			catch (Exception e)
			{
				// TODO;
				return false;
			}

			Stations.ForEach(stations => stations.ConvertStringsToStations());

			return true;
		}

		public override string ToString()
		{
			return Name;
		}
	}


	public static class StationExtensions
	{
		public static Direction GetDirection(this Station origin, Station destination)
		{
			if (origin.Line != destination.Line)
			{
				return Direction.Undefined;
			}
			else if (origin.InboundStations.Contains(destination))
			{
				return Direction.Inbound;
			}
			else if (origin.OutboundStations.Contains(destination))
			{
				return Direction.Outbound;
			}

			return Direction.Undefined;
		}


		public static RealTimeInfo GetRealTimeInfo(this Station station)
		{
			string url = string.Format(luasApiUrl, station.Abbreviation);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (WebResponse response = request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
				return (RealTimeInfo)serializer.Deserialize(stream);
			}
		}

		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";
	}
}
