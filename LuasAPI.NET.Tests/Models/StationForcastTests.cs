using LuasAPI.NET.Models;
using LuasAPI.NET.Models.RpaApiXml;
using LuasAPI.NET.Tests.StationInformation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace LuasAPI.NET.Tests.Models
{
	public class StationForcastTests
	{
		[Fact]
		public void CreateStationForcastFromRealTimeInfo_RealTimeInfoIsNull_ThrowsArgumentException()
		{
			RealTimeInfo realTimeInfoXml = null;
			Stations stations = new Stations(new UnitTestStationInformationLoader());

			Assert.Throws<ArgumentException>(() => StationForcast.CreateStationForcastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForcastFromRealTimeInfo_StationsIsNull_ThrowsArgumentException()
		{
			RealTimeInfo realTimeInfoXml = new RealTimeInfo();
			Stations stations = null;

			Assert.Throws<ArgumentException>(() => StationForcast.CreateStationForcastFromRealTimeInfo(realTimeInfoXml, stations));
		}

		[Fact]
		public void CreateStationForcastFromRealTimeInfo_()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", Name = "St. Stephen's Green", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "PAR", Name = "Parnell", IsInUse = true });
			loader.AddStations(new Station() { Abbreviation = "SAN", Name = "Sandyford", IsInUse = true });

			Stations stations = new Stations(loader);

			RealTimeInfo realTimeInfo = CreateRealTimeInfoFromXml("<stopInfo created=\"2019-12-31T12:00:00\" stop=\"St. Stephen's Green\" stopAbv=\"STS\"><message>Green Line services operating normally</message><direction name=\"Inbound\"><tram destination=\"Parnell\" dueMins=\"1\" /></direction><direction name=\"Outbound\"><tram dueMins=\"6\" destination=\"Sandyford\" /></direction></stopInfo>");

			StationForcast forcast = StationForcast.CreateStationForcastFromRealTimeInfo(realTimeInfo, stations);

			Assert.Equal(realTimeInfo.Stop, forcast.Station.Name);
			Assert.Single(forcast.InboundTrams);
			Assert.Single(forcast.OutboundTrams);
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
