using LuasAPI.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecast
{
	[XmlRoot(ElementName = "direction")]
	public class ForecastDirection
	{
		[XmlElement(ElementName = "tram")]
		public List<Tram> Trams { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string DirectionName { get; set; }

		public Direction Direction => DirectionName.ParseDirection();

		public bool NoTramsForcast => Trams.Any(t => t.NoTramsForcast);
	}


	public static class ForecastDirectionExtensions
	{
		public static ForecastDirection Inbound(this List<ForecastDirection> directions)
		{
			return directions.FirstOrDefault(d => d.Direction == Direction.Inbound);
		}

		public static ForecastDirection Outbound(this List<ForecastDirection> directions)
		{
			return directions.FirstOrDefault(d => d.Direction == Direction.Outbound);
		}
	}
}
