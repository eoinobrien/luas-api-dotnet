using System;
using System.Net.Http;

namespace LuasAPI.NET.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello to the LuasAPI.NET Example Console App.");
			Console.Write("Enter station Abbreviation: ");

			string input = Console.ReadLine();

			LuasApi api = new LuasApi();

			Console.WriteLine(api.GetStation(input));
		}
	}
}
