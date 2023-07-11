# DbConnectionFactory - a way to connect with differents databases

Creation of different database connections using the factory pattern.
This package was created to work primarily with dapper ORM and is currently only available for .NET Core 7.0.

## Package

DbConnectionFactory is a [NuGet library](https://www.nuget.org/packages/FP.DbConnectionFactory "DbConnection Factory package")  
You can also use docfx as a NuGet library:
```xml
<PackageReference Include="FP.DbConnectionFactory" Version="1.2.0" />
```

### Example usage:
```csharp

public class Employee
{
    private readonly IConfiguration _configuration;
    private readonly IDbFactory _factory;
    
    public Employee(IConfiguration configuration, IDbFactory factory)
    {
        _configuration = configuration;
        _factory = factory;
    }

    private IDbConnection connection() => _factory.CreateConnection(ServerType.PostgreSQL, _configuration).GetConnection();

    public async Task<int> AddAsync(Entity entity)
    {
         var sql = "Insert/Select/Delete/Update";
         var result = await connection().ExecuteAsync(sql, entity);
         connection().CloseConnection();
         return result;
    }
}
```
```csharp
  //Program.cs
  services.AddScoped<IDbFactory, DbFactory>();
```


#### Examples of connection strings

The following are examples for some databases

> Postgres
> - Server=ip;Port=5432;Database=database;User Id=postgres;Password=password;  

> MySql and MariaDb
> - Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

> SQL Server
> - Server=mySQLServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;

```json
 "ConnectionStrings": {
    "DefaultConnection": "connectionString"
  }
```


> **Note:** Please feel free to educate me if you have any comments.

