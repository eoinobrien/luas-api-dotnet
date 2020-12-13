namespace LuasAPI.NET.Example
{
	using System;
	using Newtonsoft.Json;

	class Program
	{
		static void Main()
		{
			LuasApi api = new LuasApi();
			var s = api.GetStation("ABB");

			//Console.WriteLine(JsonConvert.SerializeObject(api.GetAllStations()));
			//Console.WriteLine();

			Console.WriteLine(JsonConvert.SerializeObject(api.GetForecast(s)));
		}
	}
}
