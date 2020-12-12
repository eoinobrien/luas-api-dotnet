using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LuasAPI.NET;
using LuasAPI.NET.Models;
using LuasAPI.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LuasAPI.AzureFunction
{
	public static class LuasApiFunction
	{
		[FunctionName("GetAllStations")]
		public static async Task<IActionResult> GetAllStations(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations")] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("Get all stations");

			LuasApi api = new LuasApi();

			return (ActionResult)new OkObjectResult(api.GetAllStations());
		}

		[FunctionName("GetStation")]
		public static async Task<IActionResult> GetStation(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/{stationAbbreviation}")] HttpRequest req,
			string stationAbbreviation,
			ILogger log)
		{
			log.LogInformation($"Get station for '{stationAbbreviation}'");

			LuasApi api = new LuasApi();

			try
			{
				Station station = api.GetStation(stationAbbreviation);
				return new OkObjectResult(station);
			}
			catch (StationNotFoundException ex)
			{
				log.LogWarning($"StationNotFoundException for '{stationAbbreviation}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find station for: '{stationAbbreviation}'");
			}
			catch (Exception ex)
			{
				log.LogError($"Unexpected code path '{stationAbbreviation}'. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[FunctionName("GetStationForecast")]
		public static async Task<IActionResult> GetStationForecast(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/{stationAbbreviation}/forecast")] HttpRequest req,
			string stationAbbreviation,
			ILogger log)
		{
			log.LogInformation($"Get station forecast for {stationAbbreviation}");

			LuasApi api = new LuasApi();

			try
			{
				var forecast = await api.GetForecastAsync(stationAbbreviation).ConfigureAwait(false);
				return new OkObjectResult(forecast);
			}
			catch (StationNotFoundException ex)
			{
				log.LogWarning($"StationNotFoundException for '{stationAbbreviation}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find forecast for: '{stationAbbreviation}'");
			}
			catch (Exception ex)
			{
				log.LogError($"Exception thrown in GetStationForecast. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[FunctionName("GetAllStationsForecast")]
		public static async Task<IActionResult> GetAllStationsForecast(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/all/forecast")] HttpRequest req,
			ILogger log)
		{
			log.LogInformation($"Get station forecast for all stations");

			LuasApi api = new LuasApi();

			var stations = api.GetAllStations();
			var stationAbbreviations = stations.Select(s => s.Abbreviation);

			try
			{
				var allForecasts =
					await Task.WhenAll(
						stationAbbreviations.Select(
							abbreviation => api.GetForecastAsync(abbreviation)))
						.ConfigureAwait(false);

				var allForecastsDictionary = allForecasts.Select(forecast => new { forecast.Station.Abbreviation, forecast });

				return new OkObjectResult(allForecastsDictionary);
			}
			catch (StationNotFoundException ex)
			{
				log.LogWarning($"StationNotFoundException for '{ex.StationThatWasNotFound}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find forecast for: '{ex.StationThatWasNotFound}'");
			}
			catch (Exception ex)
			{
				log.LogError($"Exception thrown in GetStationForecast. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
