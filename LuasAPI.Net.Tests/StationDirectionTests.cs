using LuasAPI.NET.Stations;
using System;
using Xunit;

namespace LuasAPI.NET.Tests
{
	public class StationDirectionTests
	{
		[Fact]
		public void TwoStations_CorrectDirection()
		{
			Tuple<Station, Station, Direction>[] combos = new Tuple<Station, Station, Direction>[]
			{
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("TPT"), Station.GetFromAbbreviation("ABB"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("ABB"), Station.GetFromAbbreviation("TPT"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("ABB"), Station.GetFromAbbreviation("JER"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("ABB"), Station.GetFromAbbreviation("SAG"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("CPK"), Station.GetFromAbbreviation("SAN"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("CPK"), Station.GetFromAbbreviation("BRO"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("BRO"), Station.GetFromAbbreviation("CPK"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("CPK"), Station.GetFromAbbreviation("ABB"), Direction.Undefined),
				new Tuple<Station, Station, Direction>(Station.GetFromAbbreviation("ABB"), Station.GetFromAbbreviation("CPK"), Direction.Undefined),
			};


			foreach (Tuple<Station, Station, Direction> testCombo in combos)
			{
				Assert.Equal(testCombo.Item1.GetDirection(testCombo.Item2), testCombo.Item3);
			}
		}

	}
}
