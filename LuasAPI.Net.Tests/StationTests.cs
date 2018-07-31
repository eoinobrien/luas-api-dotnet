using LuasAPI.NET;
using System.Collections.Generic;
using Xunit;

namespace LuasAPI.NET.Tests
{
	public class StationTests
	{
		[Theory]
		[InlineData("SAN", Direction.Inbound)]
		[InlineData("SAN", Direction.Outbound)]
		public void GetDirectionStations_ValidDirection_ReturnsList(string abbreviation, Direction direction)
		{
			Station station = Stations.GetFromAbbreviation(abbreviation);
			Assert.NotNull(station.GetDirectionStations(direction));
		}


		[Theory]
		[InlineData("SAN", "STS", Direction.Inbound)]
		[InlineData("SAN", "CPK", Direction.Outbound)]
		[InlineData("SAN", "OGP", Direction.Inbound)]
		[InlineData("TRY", "OGP", Direction.Undefined)]
		[InlineData("SAG", "TPT", Direction.Inbound)]
		[InlineData("SAG", "SAN", Direction.Undefined)]
		public void GetDirectionToStation_ValidArguments(string initialStationAbbreviation, string destinationStationAbbreviation, Direction direction)
		{
			Station initalStaion = Stations.GetFromAbbreviation(initialStationAbbreviation);
			Station destinationStation = Stations.GetFromAbbreviation(destinationStationAbbreviation);

			Assert.Equal(direction, initalStaion.GetDirectionToStation(destinationStation));
		}
	}
}
