using System.Collections.Generic;
using System.Linq;

namespace LuasAPI.NET.Forecast
{
	public static class ForecastDirectionExtensions
	{
		public static IForecastDirection Inbound(this IList<IForecastDirection> directions)
		{
			return directions.FirstOrDefault(d => d.Direction == Direction.Inbound);
		}

		public static IForecastDirection Outbound(this IList<IForecastDirection> directions)
		{
			return directions.FirstOrDefault(d => d.Direction == Direction.Outbound);
		}
	}
}
