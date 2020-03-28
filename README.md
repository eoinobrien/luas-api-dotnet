# Luas.API.NET
C# Wrapper around the Luas API.

## Features
One API to rule the LUAS World.
**Work in Progress.**

* Thorough station information
* Luas Real Time Forecast
* JSON Response

### Get all stations' information

The libary contains comprehensive station information, including the latitude, longitude, and parking information.

```cs
LuasApi api = new LuasApi();
api.GetAllStations();
```

```json
[
	 {
    	"Name": "St. Stephen's Green",
        "Pronunciation": "st. stephen's green",
        "Abbreviation": "STS",
        "Line": "Green",
        "HasParking": false,
        "HasCycleParking": false,
        "Latitude": 53.3390722222222,
        "Longitude": -6.26133333333333,
        "InboundStations": [
            "DAW",
            "WES",
            "OGP",
            "OUP",
            "PAR",
            "DOM",
            "BRD",
            "GRA",
            "PHI",
            "CAB",
            "BRO"
        ],
        "OutboundStations": [
            "HAR",
            "CHA",
            "RAN",
            "BEE",
            "COW",
            "MIL",
            "WIN",
            "DUN",
            "BAL",
            "KIL",
            "STI",
            "SAN",
            "CPK",
            "GLE",
            "GAL",
            "LEO",
            "BAW",
            "RCC",
            "CCK",
            "BRE",
            "LAU",
            "CHE",
            "BRI"
        ],
        "WalkingTransfer": [],
        "IsInUse": true
    },
	...
]
```

### Get a single station's information
```cs
LuasApi api = new LuasApi();
api.GetStation("STS");
```

```json
 {
    "Name": "St. Stephen's Green",
    "Pronunciation": "st. stephen's green",
    "Abbreviation": "STS",
    "Line": "Green",
    "HasParking": false,
    "HasCycleParking": false,
    "Latitude": 53.3390722222222,
    "Longitude": -6.26133333333333,
    "InboundStations": [
        "DAW",
        "WES",
        "OGP",
        "OUP",
        "PAR",
        "DOM",
        "BRD",
        "GRA",
        "PHI",
        "CAB",
        "BRO"
    ],
    "OutboundStations": [
        "HAR",
        "CHA",
        "RAN",
        "BEE",
        "COW",
        "MIL",
        "WIN",
        "DUN",
        "BAL",
        "KIL",
        "STI",
        "SAN",
        "CPK",
        "GLE",
        "GAL",
        "LEO",
        "BAW",
        "RCC",
        "CCK",
        "BRE",
        "LAU",
        "CHE",
        "BRI"
    ],
    "WalkingTransfer": [],
    "IsInUse": true
}
```

### Get a forecast
There are a number of ways the real-time information for a station
```cs
LuasApi api = new LuasApi();
api.GetForecast("ABB");
//or
Station abbeyStreet = api.GetStation("ABB");
api.GetForecast(abbeyStreet);
```

```json
{
    "Station": {
        "Name": "Abbey Street",
        "Pronunciation": "abbey street",
        "Abbreviation": "ABB",
        "Line": "Red",
        "HasParking": false,
        "HasCycleParking": true,
        "Latitude": 53.3485888888889,
        "Longitude": -6.25817222222222,
        "InboundStations": [
            "BUS",
            "CON",
            "GDK",
            "MYS",
            "SDK",
            "TPT"
        ],
        "OutboundStations": [
            "JER",
            "FOU",
            "SMI",
            "MUS",
            "HEU",
            "JAM",
            "FAT",
            "RIA",
            "SUI",
            "GOL",
            "DRI",
            "BLA",
            "BLU",
            "KYL",
            "RED",
            "KIN",
            "BEL",
            "COO",
            "HOS",
            "TAL",
            "FET",
            "CVN",
            "CIT",
            "FOR",
            "SAG"
        ],
        "WalkingTransfer": [
            "OPG",
            "MAR"
        ],
        "IsInUse": true
    },
    "InboundTrams": [
        {
            "DestinationStation": {
                "Name": "The Point",
                "Pronunciation": "the point",
                "Abbreviation": "TPT",
                "Line": "Red",
                "HasParking": false,
                "HasCycleParking": false,
                "Latitude": 53.34835,
                "Longitude": -6.22925833333333,
                "InboundStations": [],
                "OutboundStations": [
                    "SDK",
                    "MYS",
                    "GDK",
                    "BUS",
                    "ABB",
                    "JER",
                    "FOU",
                    "SMI",
                    "MUS",
                    "HEU",
                    "JAM",
                    "FAT",
                    "RIA",
                    "SUI",
                    "GOL",
                    "DRI",
                    "BLA",
                    "BLU",
                    "KYL",
                    "RED",
                    "KIN",
                    "BEL",
                    "COO",
                    "HOS",
                    "TAL",
                    "FET",
                    "CVN",
                    "CIT",
                    "FOR",
                    "SAG"
                ],
                "WalkingTransfer": [],
                "IsInUse": true
            },
            "IsDue": false,
            "Minutes": 8
        },
        {
            "DestinationStation": {
                "Name": "The Point",
                <snipped>
            },
            "IsDue": false,
            "Minutes": 18
        }
    ],
    "OutboundTrams": [
        {
            "DestinationStation": {
                "Name": "Tallaght",
                "Pronunciation": "tallaght",
                "Abbreviation": "TAL",
                "Line": "Red",
                "HasParking": true,
                "HasCycleParking": true,
                "Latitude": 53.2874944444444,
                "Longitude": -6.37458888888889,
                "InboundStations": [
                    "HOS",
                    "COO",
                    "BEL",
                    "KIN",
                    "RED",
                    "KYL",
                    "BLU",
                    "BLA",
                    "DRI",
                    "GOL",
                    "SUI",
                    "RIA",
                    "FAT",
                    "JAM",
                    "HEU",
                    "MUS",
                    "SMI",
                    "FOU",
                    "JER",
                    "ABB",
                    "BUS",
                    "CON",
                    "GDK",
                    "MYS",
                    "SDK",
                    "TPT"
                ],
                "OutboundStations": [],
                "WalkingTransfer": [],
                "IsInUse": true
            },
            "IsDue": true,
            "Minutes": 0
        },
        {
            "DestinationStation": {
                "Name": "Tallaght",
                <snipped>
            },
            "IsDue": false,
            "Minutes": 15
        }
    ],
    "Message": "See news for information on changes to service",
    "Created": "2019-12-28T21:26:51"
}
```