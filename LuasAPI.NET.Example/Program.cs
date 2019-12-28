using LuasAPI.NET.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace LuasAPI.NET.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			LuasApi api = new LuasApi();

			var s = api.GetStation("STS");

			Console.WriteLine(JsonConvert.SerializeObject(s));

			Console.WriteLine(JsonConvert.SerializeObject(api.GetForcast(s)));
		}
	}
}