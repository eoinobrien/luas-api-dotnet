using System;

namespace LuasAPI.NET
{
	public enum Direction
	{
		Undefined = -1,
		Inbound = 0,
		Outbound = 1
	}

	public static class DirectionExtensions
	{
		public static Direction ParseDirection(this string strDirection)
		{
			return Enum.TryParse(strDirection, true, out Direction direction) ? direction : Direction.Undefined;
		}
	}
}
