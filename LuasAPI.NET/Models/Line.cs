namespace LuasAPI.NET.Models
{
	using System.Text.Json.Serialization;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum Line
	{
		Depot,
		Red,
		Green
	}
}
