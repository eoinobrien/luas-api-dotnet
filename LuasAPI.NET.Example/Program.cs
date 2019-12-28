using Newtonsoft.Json;
using System;

namespace LuasAPI.NET.Example
{
	class Program
	{
		static void Main()
		{
			LuasApi api = new LuasApi();
			var s = api.GetStation("STS");

			Console.WriteLine(JsonConvert.SerializeObject(api.GetForcast(s)));
		}
	}
}