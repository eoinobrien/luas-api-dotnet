using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LuasAPI.NET.Models.RpaApiXml
{
	[XmlRoot(ElementName = "stopInfo")]
	public class RealTimeInfo
	{
		[XmlElement(ElementName = "message")]
		public string Message { get; set; }

		[XmlElement(ElementName = "direction")]
		public List<ForecastDirection> Directions { get; set; }

		[XmlAttribute(AttributeName = "created")]
		public DateTime Created { get; set; }

		[XmlAttribute(AttributeName = "stop")]
		public string Stop { get; set; }

		[XmlAttribute(AttributeName = "stopAbv")]
		public string StopAbbreviation { get; set; }

		public static RealTimeInfo CreateFromStream(Stream stream)
		{
			using XmlReader reader = XmlReader.Create(stream);
			XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
			return (RealTimeInfo)serializer.Deserialize(reader);
		}
	}
}
