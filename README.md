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

# Notes
Using a Windows based SQL Server? Multiple Active Result Sets (MARS) is required. Go [here](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/multiple-active-result-sets-mars) for more information.
```
MultipleActiveResultSets = true
```
Example connection string:
```
Data Source=localhost;Initial Catalog=kafka-webapp;MultipleActiveResultSets=true;
```