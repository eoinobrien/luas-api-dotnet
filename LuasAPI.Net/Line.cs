using System.Collections.Generic;

namespace LuasAPI.Net
{
	public enum Line
	{
		Depot,
		Red,
		Green
	}


	public static class LineExtenstions
	{
		public static Dictionary<Line, List<Station>> LineStations = new Dictionary<Line, List<Station>>
		{
			{
				Line.Depot,
				new List<Station>
				{
					new Station("Depot", "depot", "DEP", Line.Depot, 0)
				}
			},
			{
				Line.Red,
				new List<Station>
				{
					new Station("The Point", "the point", "TPT", Line.Red, 1, Direction.Outbound),
					new Station("Spencer Dock", "spencer dock", "SDK", Line.Red, 2),
					new Station("Mayor Square - NCI", "mayor square - <say-as interpret-as=\"spell-out\">nci</say-as>", "MYS", Line.Red, 3),
					new Station("George's Dock", "george's dock", "GDK", Line.Red, 4),

					new Station("Connolly", "Connolly", "CON", Line.Red, 5),

					new Station("Busáras", "<phoneme alphabet=\"ipa\" ph=\"bʌsˠˌɑːɾˠəsˠ\">busaras</phoneme>", "BUS", Line.Red, 6),
					new Station("Abbey Street", "abbey street", "ABB", Line.Red, 7),
					new Station("Jervis", "jervis", "JER", Line.Red, 8),
					new Station("Four Courts", "four courts", "FOU", Line.Red, 9),
					new Station("Smithfield", "smithfield", "SMI", Line.Red, 10),
					new Station("Museum", "museum", "MUS", Line.Red, 11),
					new Station("Heuston", "heuston", "HEU", Line.Red, 12),
					new Station("James's", "james's", "JAM", Line.Red, 13),
					new Station("Fatima", "fatima", "FAT", Line.Red, 14),
					new Station("Rialto", "rialto", "RIA", Line.Red, 15),
					new Station("Suir Road", "suir road", "SUI", Line.Red, 16),
					new Station("Goldenbridge", "goldenbridge", "GOL", Line.Red, 17),
					new Station("Drimnagh", "drimnagh", "DRI", Line.Red, 18),
					new Station("Blackhorse", "blackhorse", "BLA", Line.Red, 19),
					new Station("Bluebell", "bluebell", "BLU", Line.Red, 20),
					new Station("Kylemore", "kylemore", "KYL", Line.Red, 21),
					new Station("Red Cow", "red cow", "RED", Line.Red, 22),
					new Station("Kingswood", "kingswood", "KIN", Line.Red, 23),
					new Station("Belgard", "belgard", "BEL", Line.Red, 24),

					new Station("Cookstown", "cookstown", "COO", Line.Red, 25),
					new Station("Hospital", "hospital", "HOS", Line.Red, 26),
					new Station("Tallaght", "tallaght", "TAL", Line.Red, 27, Direction.Inbound),

					new Station("Fettercairn", "fettercairn", "FET", Line.Red, 28),
					new Station("Cheeverstown", "cheeverstown", "CVN", Line.Red, 29),
					new Station("Citywest Campus", "citywest campus", "CIT", Line.Red, 30),
					new Station("Fortunestown", "fortunestown", "FOR", Line.Red, 31),
					new Station("Saggart", "saggart", "SAG", Line.Red, 32, Direction.Inbound),
				}
			},
			{
				Line.Green,
				new List<Station>
				{
					new Station("Broombridge", "broombridge", "BRO", Line.Green, 101, Direction.Outbound),
					new Station("Cabra", "cabra", "CAB", Line.Green, 102),
					new Station("Phibsborough", "phibsborough", "PHI", Line.Green, 103),
					new Station("Grangegorman", "grangegorman", "GRA", Line.Green, 104),
					new Station("Broadstone - DIT", "broadstone - <say-as interpret-as=\"spell-out\">dit</say-as>", "BRD", Line.Green, 105),
					new Station("Dominick", "dominick", "DOM", Line.Green, 106),

					new Station("Parnell", "parnell", "PAR", Line.Green, 107, Direction.Outbound),
					new Station("Marlborough", "marlborough", "MAR", Line.Green, 108, Direction.Outbound),
					new Station("Trinity", "trinity", "TRY", Line.Green, 109, Direction.Outbound),
					new Station("O'Connell - Upper", "o'connell - upper", "OUP", Line.Green, 110, Direction.Inbound),
					new Station("O'Connell - GPO", "o'connell - <say-as interpret-as=\"spell-out\">gpo</say-as>", "OGP", Line.Green, 111, Direction.Inbound),
					new Station("Westmoreland", "westmoreland", "WES", Line.Green, 112, Direction.Inbound),

					new Station("Dawson", "dawson", "DAW", Line.Green, 113),
					new Station("St. Stephen's Green", "st. stephen's green", "STS", Line.Green, 114),
					new Station("Harcourt", "harcourt", "HAR", Line.Green, 115),
					new Station("Charlemont", "charlemont", "CHA", Line.Green, 116),
					new Station("Ranelagh", "ranelagh", "RAN", Line.Green, 117),
					new Station("Beechwood", "beechwood", "BEE", Line.Green, 118),
					new Station("Cowper", "cowper", "COW", Line.Green, 119),
					new Station("Milltown", "milltown", "MIL", Line.Green, 120),
					new Station("Windy Arbour", "windy arbour", "WIN", Line.Green, 121),
					new Station("Dundrum", "dundrum", "DUN", Line.Green, 122),
					new Station("Balally", "balally", "BAL", Line.Green, 123),
					new Station("Kilmacud", "kilmacud", "KIL", Line.Green, 124),
					new Station("Stillorgan", "stillorgan", "STI", Line.Green, 125),
					new Station("Sandyford", "sandyford", "SAN", Line.Green, 126),
					new Station("Central Park", "central park", "CPK", Line.Green, 127),
					new Station("Glencairn", "glencairn", "GLE", Line.Green, 128),
					new Station("The Gallops", "the gallops", "GAL", Line.Green, 129),
					new Station("Leopardstown Valley", "leopardstown valley", "LEO", Line.Green, 130),
					new Station("Ballyogan Wood", "ballyogan wood", "BAW", Line.Green, 131),
					new Station("Racecourse", "racecourse", "RCC", Line.Green, 132),
					new Station("Carrickmines", "carrickmines", "CCK", Line.Green, 133),
					new Station("Brennanstown", "brennanstown", "BRE", Line.Green, 134),
					new Station("Laughanstown", "laughanstown", "LAU", Line.Green, 135),
					new Station("Cherrywood", "cherrywood", "CHE", Line.Green, 136),
					new Station("Bride's Glen", "bride's glen", "BRI", Line.Green, 137, Direction.Inbound)
				}
			}
		};


		public static IList<Station> Stations(this Line line)
		{
			List<Station> _;
			LineStations.TryGetValue(line, out _);
			return _;
		}
	}
}
