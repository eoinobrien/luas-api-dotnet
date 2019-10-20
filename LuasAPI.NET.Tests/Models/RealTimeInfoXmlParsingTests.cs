using LuasAPI.NET.Models.RpaApiXml;
using System;
using System.IO;
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
	}
}
