using System.Collections.Generic;

namespace LuasAPI.NET.Stations
{
	public interface IStation
	{
		string Name { get; }

		string Pronunciation { get; }

		string Abbreviation { get; }

		Line Line { get; }

		IList<IStation> InboundStations { get; }

		IList<IStation> OutboundStations { get; }

		IList<IStation> WalkingTransfer { get; }

		bool IsInUse { get; }
	}
}
