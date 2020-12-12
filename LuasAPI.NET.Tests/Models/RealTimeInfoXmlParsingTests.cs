using System;
using System.IO;
using LuasAPI.NET.Models.RpaApiXml;
using Xunit;

namespace LuasAPI.NET.Tests.Models
{
	public class RealTimeInfoXmlParsingTests
	{
		private Stream ConvertStringToStream(string input)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);

			writer.Write(input);
			writer.Flush();
			stream.Position = 0;

			return stream;
		}

		[Fact]
		public void CreateFromStream_EmptyString_ThrowsInvalidOperation()
		{
			string xml = string.Empty;
			Stream stream = ConvertStringToStream(xml);

			Assert.Throws<InvalidOperationException>(() => RealTimeInfo.CreateFromStream(stream));
		}


		[Fact]
		public void CreateFromStream_IncorrectXmlSchema_ThrowsInvalidOperation()
		{
			string xml = "<sample></sample>";
			Stream stream = ConvertStringToStream(xml);

			Assert.Throws<InvalidOperationException>(() => RealTimeInfo.CreateFromStream(stream));
		}


		[Fact]
		public void CreateFromStream_CorrectXmlSchema_ReturnsObject()
		{
			string xml = "<stopInfo created=\"2019-12-28T12:06:00\" stop=\"St. Stephen's Green\" stopAbv=\"STS\"><message>Green Line Service Disruption See News</message><direction name=\"Inbound\"><tram destination=\"See news for information\" dueMins=\"\" /></direction><direction name=\"Outbound\"><tram dueMins=\"6\" destination=\"Sandyford\" /></direction></stopInfo>";
			Stream stream = ConvertStringToStream(xml);

			Assert.IsType<RealTimeInfo>(RealTimeInfo.CreateFromStream(stream));
		}
	}
}
