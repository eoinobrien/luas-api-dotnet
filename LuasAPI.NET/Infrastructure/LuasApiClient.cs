using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LuasAPI.NET.Models;
using LuasAPI.NET.Models.RpaApiXml;

namespace LuasAPI.NET.Infrastructure
{
	public class LuasApiClient : ILuasRealTime
	{
		public LuasApiClient()
		{
			httpClient = new HttpClient();
		}

		private HttpClient httpClient;

		private const string luasApiUrl = "http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={0}&encrypt=false";


		public StationForcast GetRealTimeInfo(Station station)
		{
			string url = string.Format(luasApiUrl, station.Abbreviation);

			using (HttpClient client = httpClient)
			using (HttpResponseMessage response = client.GetAsync(url).Result)
			using (HttpContent content = response.Content)
			using (Stream stream = content.ReadAsStreamAsync().Result)
			{
				return (StationForcast)RealTimeInfo.CreateFromStream(stream);
			}
		}
	}
}