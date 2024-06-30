namespace LuasAPI.NET.Tests.Models
{
	using System;
	using System.IO;
	using System.Xml.Serialization;
	using LuasAPI.NET.Models;
	using LuasAPI.NET.Models.RpaApiXml;
	using LuasAPI.NET.Tests.StationInformation;
	using Xunit;

	public class StationForecastTests
	{
		[Fact]
		public void CreateStationForecastFromRealTimeInfo_RealTimeInfoIsNull_ThrowsArgumentException()
		{
			RealTimeInfo? realTimeInfoXml = null;
			var stations = new Stations(new UnitTestStationInformationLoader());

			Assert.Throws<ArgumentException>(() => StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForecastFromRealTimeInfo_StationsIsNull_ThrowsArgumentException()
		{
			var realTimeInfoXml = new RealTimeInfo();
			Stations? stations = null;

			Assert.Throws<ArgumentException>(() => StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForecastFromRealTimeInfo_1()
		{
			var loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", Name = "St. Stephen's Green", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "PAR", Name = "Parnell", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "SAN", Name = "Sandyford", IsInUse = true });

			var stations = new Stations(loader);

			RealTimeInfo realTimeInfo = CreateRealTimeInfoFromXml("<stopInfo created=\"2019-12-31T12:00:00\" stop=\"St. Stephen's Green\" stopAbv=\"STS\"><message>Green Line services operating normally</message><direction name=\"Inbound\"><tram destination=\"Parnell\" dueMins=\"1\" /></direction><direction name=\"Outbound\"><tram dueMins=\"6\" destination=\"Sandyford\" /></direction></stopInfo>");

			var forecast = StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfo, stations);

			Assert.Equal(realTimeInfo.Stop, forecast.Station.Name);
			Assert.Single(forecast.InboundTrams);
			Assert.Single(forecast.OutboundTrams);
		}

		[Fact]
		public void CreateStationForecastFromRealTimeInfo_2()
		{
			var loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STI", Name = "Stillorgan", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "BRI", Name = "Brides Glen", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "SAN", Name = "Sandyford", IsInUse = true });

			var stations = new Stations(loader);

			RealTimeInfo realTimeInfo = CreateRealTimeInfoFromXml("<stopInfo created = \"2024-06-25T00:47:32\" stop=\"Stillorgan\" stopAbv=\"STI\"><message>Green Line services operating normally</message><direction name = \"Inbound\"><tram destination=\"No trams forecast\" dueMins=\"\" /></direction><direction name = \"Outbound\"><tram dueMins=\"12\" destination=\"Brides Glen\" /></direction></stopInfo>");

			var forecast = StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfo, stations);

			Assert.Equal(realTimeInfo.Stop, forecast.Station.Name);
			Assert.Empty(forecast.InboundTrams);
			Assert.Single(forecast.OutboundTrams);
		}

		private RealTimeInfo CreateRealTimeInfoFromXml(string xml)
		{
			var serializer = new XmlSerializer(typeof(RealTimeInfo));

			return (RealTimeInfo)serializer.Deserialize(new StringReader(xml));
		}
	}
}
