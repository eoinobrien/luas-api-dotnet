namespace LuasAPI.NET.Models.RpaApiXml
{
	using System.Collections.Generic;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "direction")]
	public class ForecastDirection
	{
		private List<TramXml> trams = new List<TramXml>();

		[XmlElement(ElementName = "tram")]
		public List<TramXml> Trams { get { return trams; } }


		[XmlAttribute(AttributeName = "name")]
		public string DirectionName { get; set; }
	}
}
