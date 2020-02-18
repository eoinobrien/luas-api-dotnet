using System;
using System.IO;
using System.Threading.Tasks;
using LuasAPI.NET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LuasAPI.NET.Models;

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

        [FunctionName("GetStationForcast")]
        public static async Task<IActionResult> GetStationForcast(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/{stationAbbreviation}/forcast")] HttpRequest req,
            string stationAbbreviation,
            ILogger log)
        {
            log.LogInformation($"Get station forcast for {stationAbbreviation}");

            LuasApi api = new LuasApi();

            try
            {
                var forcast = await api.GetForcastAsync(stationAbbreviation);
                return new OkObjectResult(forcast);
            }
            catch (StationNotFoundException)
            {
                log.LogWarning($"StationNotFoundException for '{stationAbbreviation}'");
                return new NotFoundObjectResult($"Unable to find forcast for: '{stationAbbreviation}'");
            }
            catch (Exception ex)
            {
                log.LogError($"Exception thrown in GetStationForcast", ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
