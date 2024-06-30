namespace LuasAPI.NET.Models
{
	using System;
	using System.Globalization;
	using System.Text.Json;
	using System.Text.Json.Serialization;

	public class TimeConverter : JsonConverter<TimeSpan>
	{
		public const string TimeFormatString = @"hh\:mm";

		public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var timeParts = reader.GetString().Split(":");
			return new TimeSpan(int.Parse(timeParts[0], CultureInfo.InvariantCulture), int.Parse(timeParts[1], CultureInfo.InvariantCulture), 0);
		}

		public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
		{
#if NET6_0_OR_GREATER
			ArgumentNullException.ThrowIfNull(writer);
#else
			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}
#endif

			var timespanFormatted = $"{value.ToString(TimeFormatString, CultureInfo.InvariantCulture)}";
			writer.WriteStringValue(timespanFormatted);
		}
	}
}
