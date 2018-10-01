using LuasAPI.NET.Forecasts.XmlAPI;

namespace LuasAPI.NET.Forecasts
{
	public class TramForcast
	{
		public TramForcast(TramXml tramXml)
		{
			DestinationStation = Stations.GetFromNameOrAbbreviation(tramXml.Destination);
			SeeNews = tramXml.Destination.ToUpperInvariant().Contains("SEE NEWS");
			NoTramsForcast = tramXml.Destination.ToUpperInvariant() == "NO TRAMS FORECAST" || DestinationStation == null;
			IsDue = tramXml.DueMins.ToUpperInvariant() == "DUE";

			Minutes = IsDue ? 0 : 
				int.TryParse(tramXml.DueMins, out int mins) ? mins : -1;
		}


		public IStation DestinationStation { get; private set; }

		public bool IsDue { get; private set; }

		public bool NoTramsForcast { get; private set; }

		public bool SeeNews { get; private set; }

		public int Minutes { get; private set; }

		public bool TramGoesToDestination(IStation userDestination, Direction direction)
		{
			if (NoTramsForcast || DestinationStation.Line != userDestination.Line)
			{
				return false;
			}

			if (DestinationStation == userDestination)
			{
				return true;
			}

			if (direction == Direction.Inbound)
			{
				return userDestination.InboundStations.Contains(DestinationStation);
			}
			else
			{
				return userDestination.OutboundStations.Contains(DestinationStation);
			}
		}
	}
}