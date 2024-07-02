namespace LuasAPI.NET.Tests.StationInformation
{
	using System.Collections.Generic;
	using LuasAPI.NET.Models;
	using Xunit;

	public class StationInformationLoaderTests
	{
		[Fact]
		public void StationInformationLoader_LoadsStations_Correctly()
		{
			var stationLoader = new StationInformationLoader();
			Dictionary<string, Station> stations = stationLoader.Load();

			Assert.NotNull(stations);
			Assert.Equal(70, stations.Count);

			Assert.Equal("ABB", stations["ABB"].Abbreviation);
			Assert.Equal(Line.Red, stations["ABB"].Line);
			Assert.Equal("Abbey Street", stations["ABB"].Name);
			Assert.NotNull(stations["ABB"].WalkingTransfer);
			Assert.Equal(2, stations["ABB"].WalkingTransfer.Count);
			Assert.Equal("OGP", stations["ABB"].WalkingTransfer[0]);
			Assert.NotNull(stations["ABB"].InboundStations);
			Assert.NotNull(stations["ABB"].OutboundStations);
		}
	}
}
