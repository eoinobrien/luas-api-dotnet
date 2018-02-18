using LuasAPI.NET.Forecast;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LuasAPI.Net
{
	public class Station
	{
		public Station(string station, string pronunciation, string abbreviation, Line line, int stationOrder, Direction oneWayDirection = Direction.Undefined)
		{
			Name = station;
			Pronunciation = pronunciation;
			Abbreviation = abbreviation.ToUpperInvariant();
			Line = line;
			StationOrder = stationOrder;
			OneWayDirection = oneWayDirection;
		}


		public string Name { get; private set; }

		public string Pronunciation { get; private set; }

		public string Abbreviation { get; private set; }

		public Line Line { get; private set; }

		public int StationOrder { get; private set; }

		public Direction OneWayDirection { get; private set; }



		public static Station GetFromAbbreviation(string abbreviation)
		{
			if (string.IsNullOrWhiteSpace(abbreviation))
			{
				return null;
			}

			return Stations.FirstOrDefault(st => st.Abbreviation.ToUpperInvariant() == abbreviation.ToUpperInvariant());
		}


		public static Station GetFromName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}

			return Stations.FirstOrDefault(st => st.Name.ToUpperInvariant() == name.ToUpperInvariant());
		}


		public static Station GetFromNameOrAbbreviation(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return null;
			}

			return Stations.FirstOrDefault(st => st.Name.ToUpperInvariant() == input.ToUpperInvariant() || st.Abbreviation.ToUpperInvariant() == input.ToUpperInvariant());
		}


		public static List<Station> Stations
		{
			get
			{
				List<Station> stations = new List<Station>();
				stations
					.Concat(Line.Depot.Stations())
					.Concat(Line.Red.Stations())
					.Concat(Line.Green.Stations());

				return stations;
			}
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
			else if (destination.StationOrder < origin.StationOrder)
			{
				return Direction.Inbound;
			}
			else
			{
				return Direction.Outbound;
			}
		}


		public static async Task<RealTimeInfo> GetRealTimeInfo(this Station station)
		{
			string url = string.Format(luasApiUrl, station.Abbreviation);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (WebResponse response = await request.GetResponseAsync())
			using (Stream stream = response.GetResponseStream())
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
				return (RealTimeInfo)serializer.Deserialize(stream);
			}
		}

		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";
	}
}
