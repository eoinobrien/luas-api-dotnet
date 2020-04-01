using System;
using System.Globalization;
using Newtonsoft.Json;

namespace JsonTools
{
	public class TimeConverter : JsonConverter<TimeSpan>
	{
		public const string TimeFormatString = @"hh\:mm";

		public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException(nameof(writer));
			}

			var timespanFormatted = $"{value.ToString(TimeFormatString, CultureInfo.InvariantCulture)}";
			writer.WriteValue(timespanFormatted);
		}

		public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader == null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			var timeParts = ((string)reader.Value).Split(":");
			return new TimeSpan(int.Parse(timeParts[0], CultureInfo.InvariantCulture), int.Parse(timeParts[1], CultureInfo.InvariantCulture), 0);
		}
	}
}