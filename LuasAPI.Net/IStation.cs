using System.Collections.Generic;

namespace LuasAPI.NET
{
	public interface IStation
	{
		string Name { get; }

		string Pronunciation { get; }

		string Abbreviation { get; }

		Line Line { get; }

		bool HasParking { get; }

		bool HasCycleParking { get; }

		double Latitude { get; }

		double Longitude { get; }

		IList<IStation> InboundStations { get; }

		IList<IStation> OutboundStations { get; }

		IList<IStation> WalkingTransfer { get; }

		bool IsInUse { get; }
	}
}
