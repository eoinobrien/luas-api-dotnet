using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LuasAPI.NET
{
	public class StationNotFoundException : Exception
	{
		public StationNotFoundException() : base()
		{ }

		public StationNotFoundException(string message) : base(message)
		{ }

		public StationNotFoundException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}
