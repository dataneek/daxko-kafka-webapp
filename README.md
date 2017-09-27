# Usage

```
cd src/WebApp
dotnet build
dotnet run
```

# API calls

## Get list of locations

```
GET /api/locations
```

## Check member into a location

```
POST /api/locationcheckin
{
	"memberId": 1234,
	"locationId": 5678
}
```
