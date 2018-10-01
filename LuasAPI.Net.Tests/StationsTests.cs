using LuasAPI.NET;
using System;
using Xunit;

namespace LuasAPI.NET.Tests
{
	public class StationsTests
	{
		[Theory]
		[InlineData("SAN")]
		[InlineData("san")]
		[InlineData("TPT")]
		public void GetFromAbbreviation_ValidEverything_ReturnsCorrect(string abbreviation)
		{
			Assert.NotNull(Stations.GetFromAbbreviation(abbreviation));
			Assert.Equal(typeof(Station), Stations.GetFromAbbreviation(abbreviation).GetType());
			Assert.Equal(abbreviation.ToUpperInvariant(), Stations.GetFromAbbreviation(abbreviation).Abbreviation);
		}


		[Theory]
		[InlineData("s")]
		[InlineData("S")]
		[InlineData("NOt a Station")]
		public void GetFromAbbreviation_ValidArgument_InvalidValue_ReturnsNull(string abbreviation)
		{
			Assert.Null(Stations.GetFromAbbreviation(abbreviation));
		}


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void GetFromAbbreviation_InvalidArgument_Throws(string abbreviation)
		{
			Assert.Throws<ArgumentException>(() => Stations.GetFromAbbreviation(abbreviation));
		}

		[Theory]
		[InlineData("Sandyford")]
		[InlineData("The Point")]
		[InlineData("Abbey Street")]
		public void GetFromName_ValidEverything_ReturnsCorrect(string name)
		{
			Assert.NotNull(Stations.GetFromName(name));
			Assert.Equal(typeof(Station), Stations.GetFromName(name).GetType());
			Assert.Equal(name.ToUpperInvariant(), Stations.GetFromName(name).Name.ToUpperInvariant());
		}


		[Theory]
		[InlineData("s")]
		[InlineData("S")]
		[InlineData("NOt a Station")]
		public void GetFromName_ValidArgument_InvalidValue_ReturnsNull(string name)
		{
			Assert.Null(Stations.GetFromName(name));
		}


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void GetFromName_InvalidArgument_Throws(string name)
		{
			Assert.Throws<ArgumentException>(() => Stations.GetFromName(name));
		}


		[Theory]
		[InlineData("Sandyford", "SAN")]
		[InlineData("The Point", "TPT")]
		[InlineData("Abbey Street", "ABB")]
		[InlineData("SAN", "SAN")]
		[InlineData("san", "SAN")]
		[InlineData("TPT", "TPT")]
		public void GetFromNameOrAbbreviation_ValidEverything_ReturnsCorrect(string input, string abbreviation)
		{
			Assert.NotNull(Stations.GetFromNameOrAbbreviation(input));
			Assert.Equal(typeof(Station), Stations.GetFromNameOrAbbreviation(input).GetType());
			Assert.Equal(abbreviation.ToUpperInvariant(), Stations.GetFromNameOrAbbreviation(input).Abbreviation);
		}


		[Theory]
		[InlineData("s")]
		[InlineData("S")]
		[InlineData("NOt a Station")]
		public void GetFromNameOrAbbreviation_ValidArgument_InvalidValue_ReturnsNull(string input)
		{
			Assert.Null(Stations.GetFromNameOrAbbreviation(input));
		}


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void GetFromNameOrAbbreviation_InvalidArgument_Throws(string input)
		{
			Assert.Throws<ArgumentException>(() => Stations.GetFromNameOrAbbreviation(input));
		}
	}
}
