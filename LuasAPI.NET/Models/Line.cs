using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LuasAPI.NET.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Line
	{
		Depot,
		Red,
		Green
	}
}
