using System;
using System.Globalization;
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
			this.stations = stations;
		}

		private readonly Stations stations;
		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";

		public async Task<StationForecast> GetRealTimeInfoAsync(string stationAbbreviation)
		{
			if (string.IsNullOrEmpty(stationAbbreviation))
			{
				throw new ArgumentException("Can't get the forecast of a station that does not exist.", nameof(stationAbbreviation));
			}

			Uri uri = new Uri(string.Format(CultureInfo.InvariantCulture, luasApiUrl, stationAbbreviation));

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false))
			using (HttpContent content = response.Content)
			using (Stream stream = await content.ReadAsStreamAsync().ConfigureAwait(false))
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
