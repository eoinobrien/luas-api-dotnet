using LuasAPI.NET.Models.RpaApiXml;
using System;
using System.Collections.Generic;

namespace LuasAPI.NET.Models
{
	public class StationForcast
	{
		public List<TramForcast> InboundTrams { get; private set; }

		public List<TramForcast> OutboundTrams { get; private set; }

		public string Message { get; private set; }

		public DateTime Created { get; private set; }

		//public static explicit operator StationForcast(RealTimeInfo realTimeInfo)
		//{
		//	StationForcast stationForcast = new StationForcast
		//	{
		//		Message = realTimeInfo.Message,
		//		Created = realTimeInfo.Created
		//	};
		//}
	}
}
