namespace LuasAPI.NET.Tests.Models
{
	using System;
	using LuasAPI.NET.Models;
	using Xunit;

	public class TimeConverterTests
	{
		private readonly TimeConverter timeConverter = new TimeConverter();

		[Fact]
		public void TimeConverter_ReadsStringAsTimeSpan_Correctly()
		{
			var result = this.timeConverter.Read("\"6:00\"");
			Assert.Equal(new TimeSpan(6, 0, 0), result);
		}

		[Fact]
		public void TimeConverter_WritesStringAsTimeSpan_Correctly()
		{
			var result = this.timeConverter.Write(new TimeSpan(12, 0, 0));
			Assert.Equal("\"12:00\"", result);
		}
	}
}
