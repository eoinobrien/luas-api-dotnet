using System.Collections.Generic;
using System.Xml.Serialization;

namespace LuasAPI.NET.Models.RpaApiXml
{
	[XmlRoot(ElementName = "direction")]
	public class ForecastDirection
	{
		[XmlElement(ElementName = "tram")]
		public List<TramXml> Trams { get; set; }


		[XmlAttribute(AttributeName = "name")]
		public string DirectionName { get; set; }
	}
}