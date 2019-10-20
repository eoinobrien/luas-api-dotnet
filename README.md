# Luas.API.Net
C# Wrapper around the Luas API.

## Features
One API to rule the LUAS World. Work in Progress.

### Get all stations' information
```cs
LuasApi api = new LuasApi();
api.GetAllStations();
```

```json
[
	{
		"name": "St. Stephen's Green",
		...
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
	"name": "St. Stephen's Green",
	...
}
```



## How to Use
API:
	Station
		Get all
		Get 1 from Abbreviation
		Get Forcast for one


LuasAPI.GetAllStations()
LuasAPI.GetStation(Abbreviation)
	Station.GetForcast()
