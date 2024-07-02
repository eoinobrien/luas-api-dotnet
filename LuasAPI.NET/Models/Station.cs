namespace LuasAPI.NET.Models
{
	using System;
	using System.Collections.Generic;

	[Serializable]
	public class Station
	{
		public string Name { get; set; }

		public string IrishName { get; set; }

		public string Pronunciation { get; set; }

		public string Abbreviation { get; set; }

		public Line Line { get; set; }

		public bool HasParking { get; set; }

		public bool HasCycleParking { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

#pragma warning disable CA2227 // Collection properties should be read only, not changes so we can deserialise arrays correctly
		public List<string> InboundStations { get; set; }

		public List<string> OutboundStations { get; set; }

		public List<string> WalkingTransfer { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only, not changes so we can deserialise arrays correctly

		public bool IsInUse { get; set; }

		public OperatingHours OperatingHours { get; set; }
	}
}
