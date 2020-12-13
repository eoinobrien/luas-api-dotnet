namespace LuasAPI.NET.Example
{
	using System;
	using System.Diagnostics;
	using System.Threading;
	using LuasAPI.NET.Models;

	/// <summary>
	/// This class was created to see if async calls were actually faster
	/// </summary>
	class AsyncTiming
	{
		Stopwatch Stopwatch;
		LuasApi Api;

		public void Start()
		{
			Api = new LuasApi();

			string[] stations = { "DEP", "TPT", "SDK", "MYS", "GDK", "CON", "BUS", "ABB", "JER", "FOU", "SMI", "MUS", "HEU", "JAM", "FAT", "RIA", "SUI", "GOL", "DRI", "BLA", "BLU", "KYL", "RED", "KIN", "BEL", "COO", "HOS", "TAL", "FET", "CVN", "CIT", "FOR", "SAG", "BRO", "CAB", "PHI", "GRA", "BRD", "DOM", "PAR", "MAR", "TRY", "OUP", "OGP", "WES", "DAW", "STS", "HAR", "CHA", "RAN", "BEE", "COW", "MIL", "WIN", "DUN", "BAL", "KIL", "STI", "SAN", "CPK", "GLE", "GAL", "LEO", "BAW", "RCC", "CCK", "BRE", "LAU", "CHE", "BRI" };

			Test(Forecast, "Sync", stations);
			Thread.Sleep(15000);
			Test(ForecastAsync, "Async", stations);
		}

		public void Test(Func<string, long> function, string name, string[] stations)
		{
			Stopwatch = new Stopwatch();

			long times = 0;

			foreach (string station in stations)
			{
				times += function(station);
			}

			Console.WriteLine($"{name} Timing:" + (times / stations.Length));
		}

		public long Forecast(string station)
		{
			Stopwatch.Restart();

			StationForecast sf = Api.GetForecast(station);

			Stopwatch.Stop();
			Console.WriteLine($"{station}: {sf.InboundTrams[0].Minutes} ({Stopwatch.ElapsedMilliseconds})");

			return Stopwatch.ElapsedMilliseconds;
		}

		public long ForecastAsync(string station)
		{

			Stopwatch.Restart();

			StationForecast sfAsync = Api.GetForecastAsync(station).Result;

			Stopwatch.Stop();
			Console.WriteLine($"{station}: {sfAsync.InboundTrams[0].Minutes} ({Stopwatch.ElapsedMilliseconds})");

			return Stopwatch.ElapsedMilliseconds;
		}
	}
}
