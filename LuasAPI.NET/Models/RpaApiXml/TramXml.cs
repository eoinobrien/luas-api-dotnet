using System.Xml.Serialization;

namespace LuasAPI.NET.Models.RpaApiXml
{
	[XmlRoot(ElementName = "tram")]
	public class TramXml
	{
		[XmlAttribute(AttributeName = "dueMins")]
		public string DueMins { get; set; }


		[XmlAttribute(AttributeName = "destination")]
		public string Destination { get; set; }
	}
}