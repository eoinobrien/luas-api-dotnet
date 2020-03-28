using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using LuasAPI.NET.Models;
using LuasAPI.NET.Models.RpaApiXml;

namespace LuasAPI.NET.Infrastructure
{
	public class LuasForecastApiClient : IForecastClient
	{
		public LuasForecastApiClient(Stations stations)
		{
			httpClient = new HttpClient();
			this.stations = stations;
		}

		private readonly HttpClient httpClient;
		private readonly Stations stations;
		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";

		public async Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation)
		{
			string url = string.Format(luasApiUrl, stationAbbreviation);

			using (HttpClient client = httpClient)
			using (HttpResponseMessage response = await client.GetAsync(url))
			using (HttpContent content = response.Content)
			using (Stream stream = await content.ReadAsStreamAsync())
			{
				return StationForecast.CreateStationForecastFromRealTimeInfo(RealTimeInfo.CreateFromStream(stream), stations);
			}
		}

		public StationForecast GetRealTimeInfo(string stationAbbreviation)
		{
			return GetRealTimeInfoAsync(stationAbbreviation).Result;
		}
	}
}