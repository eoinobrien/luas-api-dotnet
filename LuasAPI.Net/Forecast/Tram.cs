using LuasAPI.Net;
using System.Xml.Serialization;

namespace LuasAPI.NET.Forecast
{
	[XmlRoot(ElementName = "tram")]
	public class Tram
	{
		[XmlAttribute(AttributeName = "dueMins")]
		public string DueMins { get; set; }

		[XmlAttribute(AttributeName = "destination")]
		public string Destination { get; set; }

		public Station DestinationStation => Station.GetFromNameOrAbbreviation(Destination);

		public bool IsDue => DueMins.ToUpperInvariant() == "DUE";

		public bool NoTramsForcast => Destination.ToUpperInvariant() == "NO TRAMS FORECAST";


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


	public static class TramExtensions
	{
		public static bool TramGoesToDestination(this Tram tram, Station userDestination, Direction direction)
		{
			if (tram.NoTramsForcast || tram.DestinationStation.Line != userDestination.Line)
			{
				return false;
			}

			if (direction == Direction.Inbound)
			{
				return tram.DestinationStation.StationOrder <= userDestination.StationOrder;
			}
			else
			{
				return tram.DestinationStation.StationOrder >= userDestination.StationOrder;
			}
		}
	}
}
