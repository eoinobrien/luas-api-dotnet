using System;
using System.IO;
using System.Threading.Tasks;
using LuasAPI.NET;
using LuasAPI.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LuasAPI.NET.Models;
using System.Collections.Generic;
using System.Linq;

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
			catch (StationNotFoundException)
			{
				log.LogWarning($"StationNotFoundException for '{stationAbbreviation}'");
				return new NotFoundObjectResult($"Unable to find station for: '{stationAbbreviation}'");
			}

			log.LogError($"Unexpected code path '{stationAbbreviation}'");
			return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
                var forecast = await api.GetForecastAsync(stationAbbreviation);
                return new OkObjectResult(forecast);
            }
            catch (StationNotFoundException)
            {
                log.LogWarning($"StationNotFoundException for '{stationAbbreviation}'");
                return new NotFoundObjectResult($"Unable to find forecast for: '{stationAbbreviation}'");
            }
            catch (Exception ex)
            {
                log.LogError($"Exception thrown in GetStationForecast", ex);
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
                            abbreviation => api.GetForecastAsync(abbreviation)));

                return new OkObjectResult(allForecasts);
            }
            catch (StationNotFoundException ex)
            {
                log.LogWarning($"StationNotFoundException for '{ex.StationThatWasNotFound}'");
                return new NotFoundObjectResult($"Unable to find forecast for: '{ex.StationThatWasNotFound}'");
            }
            catch (Exception ex)
            {
                log.LogError($"Exception thrown in GetStationForecast", ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
