namespace LuasAPI.NET.Tests.Models
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;
	using LuasAPI.NET.Models;
	using LuasAPI.NET.Models.RpaApiXml;
	using LuasAPI.NET.Tests.StationInformation;
	using Newtonsoft.Json;
	using Xunit;

	public class StationForecastTests
	{
		[Fact]
		public void CreateStationForecastFromRealTimeInfo_RealTimeInfoIsNull_ThrowsArgumentException()
		{
			RealTimeInfo realTimeInfoXml = null;
			Stations stations = new Stations(new UnitTestStationInformationLoader());

			Assert.Throws<ArgumentException>(() => StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForecastFromRealTimeInfo_StationsIsNull_ThrowsArgumentException()
		{
			RealTimeInfo realTimeInfoXml = new RealTimeInfo();
			Stations stations = null;

			Assert.Throws<ArgumentException>(() => StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForecastFromRealTimeInfo_()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", Name = "St. Stephen's Green", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "PAR", Name = "Parnell", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "SAN", Name = "Sandyford", IsInUse = true });

			Stations stations = new Stations(loader);

			RealTimeInfo realTimeInfo = CreateRealTimeInfoFromXml("<stopInfo created=\"2019-12-31T12:00:00\" stop=\"St. Stephen's Green\" stopAbv=\"STS\"><message>Green Line services operating normally</message><direction name=\"Inbound\"><tram destination=\"Parnell\" dueMins=\"1\" /></direction><direction name=\"Outbound\"><tram dueMins=\"6\" destination=\"Sandyford\" /></direction></stopInfo>");

			StationForecast forecast = StationForecast.CreateStationForecastFromRealTimeInfo(realTimeInfo, stations);

			Assert.Equal(realTimeInfo.Stop, forecast.Station.Name);
			Assert.Single(forecast.InboundTrams);
			Assert.Single(forecast.OutboundTrams);
		}

		private RealTimeInfo CreateRealTimeInfoFromXml(string xml)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));

			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
			{
				return (RealTimeInfo)serializer.Deserialize(stream);
			}
		}
	}
}
