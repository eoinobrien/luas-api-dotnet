using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuasAPI.NET.Models
{
	[Serializable]
	public class Station
	{
		public string Name { get; set; }

		public string Pronunciation { get; set; }

		public string Abbreviation { get; set; }

		public Line Line { get; set; }

		public bool HasParking { get;  set; }

		public bool HasCycleParking { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public List<string> inboundStations { get; set; }

		public List<string> outboundStations { get; set; }

		public List<string> walkingTransfer { get; set; }

		public bool IsInUse { get; set; }
	}
}
