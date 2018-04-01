using LuasAPI.NET.Forecast;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace LuasAPI.NET.Stations
{
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


		public static IRealTimeInfo GetRealTimeInfo(this Station station)
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
