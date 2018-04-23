using LuasAPI.NET.Stations;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecast
{
	[XmlRoot(ElementName = "tram")]
	public class Tram : ITram
	{
		[XmlAttribute(AttributeName = "dueMins")]
		public string DueMins { get; set; }

		[XmlAttribute(AttributeName = "destination")]
		public string Destination { get; set; }

		public IStation DestinationStation => Station.GetFromNameOrAbbreviation(Destination);

		public bool IsDue => DueMins.ToUpperInvariant() == "DUE";

		public bool NoTramsForcast => Destination.ToUpperInvariant() == "NO TRAMS FORECAST" || DestinationStation == null;

		public bool SeeNews => Destination.ToUpperInvariant().Contains("SEE NEWS");


		public int Minutes
		{
			get
			{
				if (IsDue)
				{
					return 0;
				}

				return int.TryParse(DueMins, out int mins) ? mins : -1;
			}
		}
	}
}
