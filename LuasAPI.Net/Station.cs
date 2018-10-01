using LuasAPI.NET.Forecasts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace LuasAPI.NET
{
	public class Station : IStation
	{
		public Station(string station, string pronunciation, string abbreviation, Line line)
		{
			Name = station;
			Pronunciation = pronunciation;
			Abbreviation = abbreviation.ToUpperInvariant();
			Line = line;
		}

		[JsonProperty(PropertyName = "name")]
		public string Name { get; private set; }

		[JsonProperty(PropertyName = "pronunciation")]
		public string Pronunciation { get; private set; }

		[JsonProperty(PropertyName = "abbreviation")]
		public string Abbreviation { get; private set; }

		[JsonProperty(PropertyName = "line")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Line Line { get; private set; }

		[JsonProperty(PropertyName = "hasParking")]
		public bool HasParking { get; private set; }

		[JsonProperty(PropertyName = "hasCycleParking")]
		public bool HasCycleParking { get; private set; }

		[JsonProperty(PropertyName = "latitude")]
		public double Latitude { get; private set; }

		[JsonProperty(PropertyName = "longitude")]
		public double Longitude { get; private set; }

		public IList<IStation> InboundStations { get; private set; }

		public IList<IStation> OutboundStations { get; private set; }

		public IList<IStation> WalkingTransfer { get; private set; }

		[JsonProperty(PropertyName = "inbound-stations")]
		private List<string> inboundStations { get; set; }

		[JsonProperty(PropertyName = "outbound-stations")]
		private List<string> outboundStations { get; set; }

		[JsonProperty(PropertyName = "walking-transfer")]
		private List<string> walkingTransfer { get; set; }

		[JsonProperty(PropertyName = "in-use")]
		public bool IsInUse { get; private set; }

		public void ConvertStringsToStations()
		{
			InboundStations = new List<IStation>();
			OutboundStations = new List<IStation>();
			WalkingTransfer = new List<IStation>();

			inboundStations.ForEach(st => InboundStations.Add(Stations.GetFromAbbreviation(st)));
			outboundStations.ForEach(st => OutboundStations.Add(Stations.GetFromAbbreviation(st)));
			walkingTransfer.ForEach(st => WalkingTransfer.Add(Stations.GetFromAbbreviation(st)));
		}

		public IList<IStation> GetDirectionStations(Direction direction)
		{
			if (!Stations.Loaded)
			{
				throw new InvalidOperationException("Stations have not been loaded successfully.");
			}

			if (direction == Direction.Inbound)
			{
				return InboundStations;
			}
			else if (direction == Direction.Outbound)
			{
				return OutboundStations;
			}

			return new List<IStation>();
		}

		public Direction GetDirectionToStation(Station destination)
		{
			if (Line != destination.Line)
			{
				return Direction.Undefined;
			}
			else if (InboundStations.Contains(destination))
			{
				return Direction.Inbound;
			}
			else if (OutboundStations.Contains(destination))
			{
				return Direction.Outbound;
			}

			return Direction.Undefined;
		}

		public Forcast GetForcast()
		{
			return new Forcast(this);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}