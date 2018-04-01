using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecast
{
	[XmlRoot(ElementName = "direction")]
	public class ForecastDirection : IForecastDirection
	{
		[XmlElement(ElementName = "tram")]
		public List<Tram> Trams { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string DirectionName { get; set; }

		public Direction Direction => DirectionName.ParseDirection();

		public bool NoTramsForcast => Trams.Any(t => t.NoTramsForcast);
	}
}
