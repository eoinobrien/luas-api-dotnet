using System;
using System.Collections.Generic;

namespace LuasAPI.NET.Models
{
	[Serializable]
	public class Station
	{
		public string Name { get; set; }

		public string IrishName { get; set; }

		public string Pronunciation { get; set; }

		public string Abbreviation { get; set; }

		public Line Line { get; set; }

		public bool HasParking { get;  set; }

		public bool HasCycleParking { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public List<string> InboundStations { get; set; }

		public List<string> OutboundStations { get; set; }

		public List<string> WalkingTransfer { get; set; }

		public bool IsInUse { get; set; }
	}
}
