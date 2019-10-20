using System;
using System.Collections.Generic;
using System.Text;

namespace LuasAPI.NET.Models
{
	public class TramForcast
	{
		public Station DestinationStation { get; private set; }

		public bool IsDue { get; private set; }

		public bool NoTramsForcast { get; private set; }

		public int Minutes { get; private set; }
	}
}
