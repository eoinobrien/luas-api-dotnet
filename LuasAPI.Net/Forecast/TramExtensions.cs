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

			if (direction == Direction.Inbound)
			{
				return tram.DestinationStation.InboundStations.Contains(userDestination);
			}
			else
			{
				return tram.DestinationStation.OutboundStations.Contains(userDestination);
			}
		}
	}
}
