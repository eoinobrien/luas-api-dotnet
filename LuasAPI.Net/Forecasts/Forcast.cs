using LuasAPI.NET.Forecasts.XmlAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecasts
{
	public class Forcast
	{
		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";

		public Forcast(Station station)
			: this(station.Abbreviation)
		{
		}

		public Forcast(string stationAbbreviation)
		{
			RealTimeInfo realTimeInfo = GetRealTimeInfo(stationAbbreviation);

			Message = realTimeInfo.Message;
			CreatedDate = realTimeInfo.Created;

			foreach (ForecastDirection forecastDirection in realTimeInfo.Directions)
			{
				Direction direction = forecastDirection.DirectionName.ParseDirection();

				this[direction] = forecastDirection.Trams.Select(t => new TramForcast(t)).ToList();
			}
		}

		public List<TramForcast> InboundTrams { get; private set; }

		public List<TramForcast> OutboundTrams { get; private set; }

		public string Message { get; private set; }

		public DateTime CreatedDate { get; private set; }

		public List<TramForcast> this[Direction direction]
		{
			get
			{
				return GetTrams(direction);
			}
			set
			{
				SetTrams(direction, value);
			}
		}

		public List<TramForcast> GetTrams(Direction direction)
		{
			if (direction == Direction.Undefined)
			{
				return new List<TramForcast>();
			}
			else if (direction == Direction.Inbound)
			{
				return InboundTrams;
			}
			else
			{
				return OutboundTrams;
			}
		}

		public void SetTrams(Direction direction, List<TramForcast> trams)
		{
			if (direction == Direction.Undefined)
			{
				return;
			}
			else if (direction == Direction.Inbound)
			{
				InboundTrams = trams;
			}
			else
			{
				OutboundTrams = trams;
			}
		}

		private RealTimeInfo GetRealTimeInfo(string abbreviation)
		{
			string url = string.Format(luasApiUrl, abbreviation);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (WebResponse response = request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
				return (RealTimeInfo)serializer.Deserialize(stream);
			}
		}
	}
}