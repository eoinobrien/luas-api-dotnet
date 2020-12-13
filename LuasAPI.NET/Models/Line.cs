namespace LuasAPI.NET.Models
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	[JsonConverter(typeof(StringEnumConverter))]
	public enum Line
	{
		Depot,
		Red,
		Green
	}
}
