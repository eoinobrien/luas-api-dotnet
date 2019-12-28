using LuasAPI.NET.Models;
using LuasAPI.NET.Tests.StationInformation;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Xunit;

namespace LuasAPI.NET.Tests
{
	public class StationsTests
	{
		[Fact]
		public void GetAllStations_NoStations_ReturnsEmptyList()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Stations stations = new Stations(loader);

			Assert.IsType<List<Station>>(stations.GetAllStations());
			Assert.Empty(stations.GetAllStations());
		}

		[Fact]
		public void GetAllStations_OneStation_ReturnsListWithOneItem()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", IsInUse = true });

			Stations stations = new Stations(loader);

			var result = stations.GetAllStations();
			Assert.IsType<List<Station>>(result);
			Assert.Single(result);
		}

		[Fact]
		public void GetAllStations_ReturnOnlyThoseInUse_OneStationNotInUse_ReturnsOneStation()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", IsInUse = false });

			Stations stations = new Stations(loader);

			var result = stations.GetAllStations();
			Assert.IsType<List<Station>>(result);
			Assert.Empty(result);
		}

		[Fact]
		public void GetAllStations_DontReturnOnlyThoseInUse_OneStationNotInUse_ReturnsEmptyList()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(new Station() { Abbreviation = "STS", IsInUse = false });

			Stations stations = new Stations(loader);

			var result = stations.GetAllStations(returnOnlyStationsInUse: false);
			Assert.IsType<List<Station>>(result);
			Assert.Single(result);
		}

		[Fact]
		public void GetAllStations_ReturnOnlyThoseInUse_OneStationInUse_TwoStationsNotInUse_ReturnsOneStation()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(
				new Station() { Abbreviation = "STS", IsInUse = false },
				new Station() { Abbreviation = "SAN", IsInUse = true },
				new Station() { Abbreviation = "HAR", IsInUse = false });

			Stations stations = new Stations(loader);

			var result = stations.GetAllStations();
			Assert.IsType<List<Station>>(result);
			Assert.Single(result);
		}

		[Fact]
		public void GetAllStations_DontReturnOnlyThoseInUse_OneStationInUse_TwoStationsNotInUse_ReturnsThreeStations()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();
			loader.AddStations(
				new Station() { Abbreviation = "STS", IsInUse = false },
				new Station() { Abbreviation = "SAN", IsInUse = true },
				new Station() { Abbreviation = "HAR", IsInUse = false });

			Stations stations = new Stations(loader);

			var result = stations.GetAllStations(returnOnlyStationsInUse: false);
			Assert.IsType<List<Station>>(result);
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public void GetStation_WhiteSpaceAbbreviation_ThrowsArgumentException()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Stations stations = new Stations(loader);

			Assert.Throws<ArgumentException>(() => stations.GetStation(string.Empty));
		}

		[Fact]
		public void GetStation_NullAbbreviation_ThrowsArgumentException()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Stations stations = new Stations(loader);

			Assert.Throws<ArgumentException>(() => stations.GetStation(null));
		}

		[Fact]
		public void GetStation_StationNotFound_ThrowsException()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Stations stations = new Stations(loader);

			Assert.Throws<StationNotFoundException>(() => stations.GetStation("STS"));
		}

		[Fact]
		public void GetStation_UppercaseQueryStationExists_ReturnsStation()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Station station = new Station { Name = "St. Stephen's Green", Abbreviation = "STS" };
			loader.AddStations(station);

			Stations stations = new Stations(loader);

			Assert.Equal(station, stations.GetStation(station.Abbreviation));
		}

		[Fact]
		public void GetStation_LowercaseQueryStationExists_ReturnsStation()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Station station = new Station { Name = "St. Stephen's Green", Abbreviation = "STS" };
			loader.AddStations(station);

			Stations stations = new Stations(loader);

			Assert.Equal(station, stations.GetStation(station.Abbreviation.ToLowerInvariant()));
		}

		[Fact]
		public void GetStation_MoreThanOneStationAvailable_ReturnsCorrectStation()
		{
			UnitTestStationInformationLoader loader = new UnitTestStationInformationLoader();

			Station alternativeStation = new Station { Name = "Harcourt", Abbreviation = "HAR" };
			Station stationToQuery = new Station { Name = "St. Stephen's Green", Abbreviation = "STS" };
			loader.AddStations(alternativeStation, stationToQuery);

			Stations stations = new Stations(loader);

			Assert.Equal(stationToQuery, stations.GetStation(stationToQuery.Abbreviation));
		}
	}
}
