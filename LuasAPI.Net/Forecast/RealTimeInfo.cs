using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecast
{
	[XmlRoot(ElementName = "stopInfo")]
	public class RealTimeInfo
	{
		[XmlElement(ElementName = "message")]
		public string Message { get; set; }

		public bool ServicesOperatingNormally => Message.ToUpperInvariant().Contains("LINE SERVICES OPERATING NORMALLY");

		[XmlElement(ElementName = "direction")]
		public List<ForecastDirection> Directions { get; set; }

		[XmlAttribute(AttributeName = "created")]
		public DateTime Created { get; set; }

		[XmlAttribute(AttributeName = "stop")]
		public string Stop { get; set; }

		[XmlAttribute(AttributeName = "stopAbv")]
		public string StopAbbreviation { get; set; }
	}
}
