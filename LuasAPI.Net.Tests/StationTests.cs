using LuasAPI.NET.Stations;
using Xunit;

namespace LuasAPI.NET.Tests
{
	public class StationTests
	{
		[Fact]
		public void HttpCallReturnsAResponse()
		{
			Station st = Station.GetFromNameOrAbbreviation("CPK");
			Assert.NotNull(st.GetRealTimeInfo());
		}
	}
}
