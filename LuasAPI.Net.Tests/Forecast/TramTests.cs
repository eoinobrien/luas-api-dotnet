using LuasAPI.NET.Forecast;
using LuasAPI.NET.Stations;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xunit;

namespace LuasAPI.NET.Tests.Forecast
{
	public class TramTests
	{
		[Fact]
		public void NoTramsForecastInAll_BothNoTramForecastsReturnsTrue()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TPT_NoTramsForecast_BothDirections.xml");

			Assert.True(realTimeInfo.Directions.All(d => d.Trams.All(t => t.NoTramsForcast)));
			Assert.True(realTimeInfo.Directions.All(d => d.NoTramsForcast));
		}


		[Fact]
		public void NoTramsInbound_TwoTramsOutbound_InboundNoTramsForcast_AndVerifyOutboundIsInt()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TPT_NoTramsInbound_2TramsOutbound.xml");

			Assert.True(realTimeInfo.Directions.ElementAt((int) Direction.Inbound).NoTramsForcast);
			Assert.IsType<int>(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.First().Minutes);
		}


		[Fact]
		public void NoTramsInbound_TwoTramsOutboundOneDue_InboundNoTramsForcast_OutboundIsDueBool()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TPT_NoTramsInbound_2TramsOutboundOneDue.xml");

			Assert.True(realTimeInfo.Directions.ElementAt((int)Direction.Inbound).NoTramsForcast);

			Assert.True(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.First().IsDue);
			Assert.IsType<int>(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.First().Minutes);
			Assert.Equal(0, realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.First().Minutes);

			Assert.False(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.ElementAt(1).IsDue);
			Assert.IsType<int>(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.ElementAt(1).Minutes);
			Assert.NotEqual(0, realTimeInfo.Directions.ElementAt((int)Direction.Outbound).Trams.ElementAt(1).Minutes);
		}


		[Fact]
		public void NoTramsInbound_NoTramsOutboundOneDue_SeeNews()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TAL_SeeNews_NoTrams.xml");

			Assert.True(realTimeInfo.Directions.ElementAt((int)Direction.Inbound).NoTramsForcast);
			Assert.True(realTimeInfo.Directions.ElementAt((int)Direction.Outbound).NoTramsForcast);

			Assert.Contains(realTimeInfo.Directions, d => d.SeeNews);
		}


		private RealTimeInfo ParseExampleRealTimeInfo(string path)
		{
			using (StreamReader reader = new StreamReader(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
				return (RealTimeInfo)serializer.Deserialize(reader);
			}
		}


		[Fact]
		public void TramForcast_ParseDirectionCorrectly()
		{
			Tuple<string, Station>[] testCases = new Tuple<string, Station>[]
			{
				new Tuple<string, Station>("bride's glen", Station.GetFromName("Bride's Glen")),
				new Tuple<string, Station>("sandyford", Station.GetFromName("Sandyford")),
				new Tuple<string, Station>("the point", Station.GetFromName("The Point")),
				new Tuple<string, Station>("parnell", Station.GetFromName("Parnell")),
			};


			foreach (Tuple<string, Station> testCase in testCases)
			{
				Tram tram = new Tram() { Destination = testCase.Item1 };
				Assert.Equal(tram.DestinationStation.Name, testCase.Item2.Name);
			}
		}


		[Fact]
		public void TramForcast_ParseDirectionToDefault()
		{
			string[] testCases = new string[]
			{
				 "test",
				 "brides glen",
			};


			foreach (string testCase in testCases)
			{
				Tram tram = new Tram() { Destination = testCase };
				Assert.Null(tram.DestinationStation);
			}
		}


		[Fact]
		public void TramForcast_TramGoesToDestination()
		{
			Tuple<Tram, Direction, Station>[] testCases = new Tuple<Tram, Direction, Station>[]
			{
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Bride's Glen" }, Direction.Outbound, Station.GetFromAbbreviation("CPK")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Parnell" }, Direction.Inbound, Station.GetFromAbbreviation("STS")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "St. Stephen's Green" }, Direction.Inbound, Station.GetFromAbbreviation("CPK")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Heuston" }, Direction.Outbound, Station.GetFromAbbreviation("MUS")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Heuston" }, Direction.Outbound, Station.GetFromAbbreviation("HEU")),
			};


			foreach (Tuple<Tram, Direction, Station> testCase in testCases)
			{
				Assert.True(testCase.Item1.TramGoesToDestination(testCase.Item3, testCase.Item2),
					$"Tram with Destination '{testCase.Item1.Destination}' should go to Destination '{testCase.Item3}'");
			}
		}


		[Fact]
		public void TramForcast_TramDoesntGoToDestination()
		{
			Tuple<Tram, Direction, Station>[] testCases = new Tuple<Tram, Direction, Station>[]
			{
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Sandyford" }, Direction.Outbound, Station.GetFromAbbreviation("CPK")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Parnell" }, Direction.Inbound, Station.GetFromAbbreviation("ABB")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Parnell" }, Direction.Inbound, Station.GetFromAbbreviation("MUS")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Heuston" }, Direction.Outbound, Station.GetFromAbbreviation("TAL")),
				new Tuple<Tram, Direction, Station>( new Tram() { Destination = "Muesum" }, Direction.Inbound, Station.GetFromAbbreviation("HEU")),
			};


			foreach (Tuple<Tram, Direction, Station> testCase in testCases)
			{
				Assert.False(testCase.Item1.TramGoesToDestination(testCase.Item3, testCase.Item2),
					$"Tram with Destination '{testCase.Item1.Destination}' should not go to Destination '{testCase.Item3}'");
			}
		}
	}
}
