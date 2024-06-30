namespace LuasAPI.NET.Example
{
	using System;
	using System.Text.Json;

	class Program
	{
		static void Main()
		{
			LuasApi api = new LuasApi();
			var s = api.GetStation("ABB");

			//Console.WriteLine(JsonSerializer.Serialize(api.GetAllStations()));
			//Console.WriteLine();

			Console.WriteLine(JsonSerializer.Serialize(api.GetForecast(s)));
		}
	}
}
