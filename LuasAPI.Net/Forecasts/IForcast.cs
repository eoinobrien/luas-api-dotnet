using System.Collections.Generic;

namespace LuasAPI.NET.Forecasts
{
	public interface IForcast
	{
		string Message { get; set; }


		List<TramForcast> InboundTrams { get; set; }


		List<TramForcast> OutboundTrams { get; set; }


		List<TramForcast> GetTrams(Direction direction);
	}
}
