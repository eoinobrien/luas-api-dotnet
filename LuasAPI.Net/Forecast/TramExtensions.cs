using LuasAPI.NET.Stations;

namespace LuasAPI.NET.Forecast
{
	public static class TramExtensions
	{
		public static bool TramGoesToDestination(this ITram tram, IStation userDestination, Direction direction)
		{
			if (tram.NoTramsForcast || tram.DestinationStation.Line != userDestination.Line)
			{
				return false;
			}

			if (tram.DestinationStation == userDestination)
			{
				return true;
			}

			if (direction == Direction.Inbound)
			{
				return userDestination.InboundStations.Contains(tram.DestinationStation);
			}
			else
			{
				return userDestination.OutboundStations.Contains(tram.DestinationStation);
			}
		}
	}
}
