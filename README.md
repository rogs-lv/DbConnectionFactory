# DbConnectionFactory Library
Creation of different database connections using the factory pattern

## Use the NuGet Package

Download this package for your project in [DbConnection Factory](https://www.nuget.org/packages/FP.DbConnectionFactory "DbConnection Factory package").
or you can also use docfx as a NuGet library:
```xml
<PackageReference Include="FP.DbConnectionFactory" Version="1.1.0" />
```

### Example
```cs

//Class
public class Demo
{
    private readonly IConfiguration _configuration;
    private readonly IDbFactory _factory;
    
    public Demo(IConfiguration configuration, IDbFactory factory)
    {
        _factory = factory;
    }
    ///ServerType.SqlServer
    ///ServerType.PostgreSQL
    ///ServerType.MySql/MariaDb
    private IDbConnection connection() => _factory.CreateConnection(ServerType.PostgreSQL, _configuration).GetConnection();

    public async Task<int> AddAsync(Entity entity)
    {
         var sql = "Insert/Select/Delete/Update";
         var result = await connection().ExecuteAsync(sql, entity);
         return result;
    }
}
```
```cs
  //Program.cs
  services.AddSingleton<IDbFactory, DbFactory>();
```

```json
 ///Postgres > Server=ip;Port=5432;Database=database;User Id=postgres;Password=password;
 ///MySql|MariaDb > Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
 ///SqlServer > Server=mySQLServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
 
 "ConnectionStrings": {
    "DefaultConnection": "Depending of database"
  }
```

> Please feel free to educate me if you have any comments.

