var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("sqlserver");
var mssqlDb = mssql.AddDatabase("AirlineDb");

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");
var rabbitUserName = builder.AddParameter("RabbitMQLogin");
var rabbitPassword = builder.AddParameter("RabbitMQPassword");
var rabbitMqQueue = builder.AddParameter("RabbitMQQueue");

var rabbitMq = builder.AddRabbitMQ("airlineapp-rabbitmq",
        userName: rabbitUserName,
        password: rabbitPassword)
    .WithManagementPlugin();

builder.AddProject<Projects.AirlineApp_Api>("AirlineAppAPI")
    .WithReference(mssqlDb, "DefaultConnection")
    .WithReference(rabbitMq) 
    .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue)
    .WaitFor(mssqlDb)
    .WaitFor(rabbitMq); 

builder.AddProject<Projects.AirlineApp_Generator_RabbitMq_Host>("airlineapp-generator-rabbitmq-host")
    .WithReference(rabbitMq)
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:PayloadLimit", payloadLimit)
    .WithEnvironment("Generator:WaitTime", waitTime)
    .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue)
    .WaitFor(rabbitMq); 

builder.Build().Run();