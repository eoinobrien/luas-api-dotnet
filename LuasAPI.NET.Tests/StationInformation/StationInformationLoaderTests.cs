namespace LuasAPI.NET.Tests.StationInformation
{
	using LuasAPI.NET.Models;
	using Xunit;

	public class StationInformationLoaderTests
	{
		[Fact]
		public void StationInformationLoader_LoadsStations_Correctly()
		{
			var stationLoader = new StationInformationLoader();
			var stations = stationLoader.Load();

			Assert.NotNull(stations);
			Assert.Equal(70, stations.Count);

			Assert.Equal("ABB", stations["ABB"].Abbreviation);
			Assert.Equal(Line.Red, stations["ABB"].Line);
			Assert.Equal("Abbey Street", stations["ABB"].Name);
		}
	}
}
