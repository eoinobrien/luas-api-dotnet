using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LuasAPI.NET
{
	public class StationNotFoundException : Exception
	{
		public string StationThatWasNotFound { get; }

		public StationNotFoundException(string stationThatWasNotFound) : this(stationThatWasNotFound, null, null)
		{ }

		public StationNotFoundException(string stationThatWasNotFound, string message) : this(stationThatWasNotFound, message, null)
		{ }

		public StationNotFoundException(string stationThatWasNotFound, string message, Exception innerException) : base(message, innerException)
		{
			StationThatWasNotFound = stationThatWasNotFound;
		}
	}
}
