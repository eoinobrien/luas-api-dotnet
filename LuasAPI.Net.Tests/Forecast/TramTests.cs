using LuasAPI.NET.Forecast;
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

			Assert.True(realTimeInfo.Directions.TrueForAll(d => d.Trams.TrueForAll(t => t.NoTramsForcast)));
			Assert.True(realTimeInfo.Directions.TrueForAll(d => d.NoTramsForcast));
		}


		[Fact]
		public void NoTramsInbound_TwoTramsOutbound_InboundNoTramsForcast_AndVerifyOutboundIsInt()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TPT_NoTramsInbound_2TramsOutbound.xml");

			Assert.True(realTimeInfo.Directions.Inbound().NoTramsForcast);
			Assert.IsType<int>(realTimeInfo.Directions.Outbound().Trams.First().Minutes);
		}


		[Fact]
		public void NoTramsInbound_TwoTramsOutboundOneDue_InboundNoTramsForcast_OutboundIsDueBool()
		{
			RealTimeInfo realTimeInfo = ParseExampleRealTimeInfo(@".\Forecast\Examples\TPT_NoTramsInbound_2TramsOutboundOneDue.xml");

			Assert.True(realTimeInfo.Directions.Inbound().NoTramsForcast);

			Assert.True(realTimeInfo.Directions.Outbound().Trams.First().IsDue);
			Assert.IsType<int>(realTimeInfo.Directions.Outbound().Trams.First().Minutes);
			Assert.Equal(0, realTimeInfo.Directions.Outbound().Trams.First().Minutes);

			Assert.False(realTimeInfo.Directions.Outbound().Trams.ElementAt(1).IsDue);
			Assert.IsType<int>(realTimeInfo.Directions.Outbound().Trams.ElementAt(1).Minutes);
			Assert.NotEqual(0, realTimeInfo.Directions.Outbound().Trams.ElementAt(1).Minutes);
		}


		private RealTimeInfo ParseExampleRealTimeInfo(string path)
		{
			using (StreamReader reader = new StreamReader(path))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RealTimeInfo));
				return (RealTimeInfo)serializer.Deserialize(reader);
			}
		}
	}
}
