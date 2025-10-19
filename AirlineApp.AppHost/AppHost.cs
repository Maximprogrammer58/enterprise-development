var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("sqlserver");
var mssqlDb = mssql.AddDatabase("AirlineDb");

var api = builder.AddProject<Projects.AirlineApp_API>("AirlineAppAPI")
    .WithReference(mssqlDb, "DefaultConnection")
    .WaitFor(mssqlDb);

builder.Build().Run();

