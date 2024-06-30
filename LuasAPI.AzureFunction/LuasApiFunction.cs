namespace LuasApi.AzureFunction
{
	using LuasAPI.NET;
	using LuasAPI.NET.Models;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Azure.Functions.Worker;
	using Microsoft.Extensions.Logging;

	public class LuasApiFunction
	{
		private readonly ILogger<LuasApiFunction> _logger;

		public LuasApiFunction(ILogger<LuasApiFunction> logger)
		{
			_logger = logger;
		}

		[Function("GetAllStations")]
		public IActionResult GetAllStations([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations")] HttpRequest req)
		{
			this._logger.LogInformation("Get all stations");

			LuasApi api = new LuasApi();

			return new OkObjectResult(api.GetAllStations());
		}

		[Function("GetStation")]
		public IActionResult GetStation(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/{stationAbbreviation}")] HttpRequest req,
			string stationAbbreviation)
		{
			this._logger.LogInformation($"Get station for '{stationAbbreviation}'");

			LuasApi api = new LuasApi();

			try
			{
				Station station = api.GetStation(stationAbbreviation);
				return new OkObjectResult(station);
			}
			catch (StationNotFoundException ex)
			{
				this._logger.LogWarning($"StationNotFoundException for '{stationAbbreviation}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find station for: '{stationAbbreviation}'");
			}
			catch (Exception ex)
			{
				this._logger.LogError($"Unexpected code path '{stationAbbreviation}'. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[Function("GetStationForecast")]
		public async Task<IActionResult> GetStationForecast(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/{stationAbbreviation}/forecast")] HttpRequest req,
			string stationAbbreviation)
		{
			this._logger.LogInformation($"Get station forecast for {stationAbbreviation}");

			LuasApi api = new LuasApi();

			try
			{
				var forecast = await api.GetForecastAsync(stationAbbreviation).ConfigureAwait(false);
				return new OkObjectResult(forecast);
			}
			catch (StationNotFoundException ex)
			{
				this._logger.LogWarning($"StationNotFoundException for '{stationAbbreviation}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find forecast for: '{stationAbbreviation}'");
			}
			catch (Exception ex)
			{
				this._logger.LogError($"Exception thrown in GetStationForecast. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[Function("GetAllStationsForecast")]
		public async Task<IActionResult> GetAllStationsForecast(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stations/all/forecast")] HttpRequest req)
		{
			this._logger.LogInformation($"Get station forecast for all stations");

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
				this._logger.LogWarning($"StationNotFoundException for '{ex.StationThatWasNotFound}'. Exception: {ex}");
				return new NotFoundObjectResult($"Unable to find forecast for: '{ex.StationThatWasNotFound}'");
			}
			catch (Exception ex)
			{
				this._logger.LogError($"Exception thrown in GetStationForecast. Exception: {ex}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
