var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("sqlserver");
var mssqlDb = mssql.AddDatabase("AirlineDb");

builder.AddProject<Projects.AirlineApp_Api>("AirlineAppAPI")
    .WithReference(mssqlDb, "DefaultConnection")
    .WaitFor(mssqlDb);

builder.Build().Run();

