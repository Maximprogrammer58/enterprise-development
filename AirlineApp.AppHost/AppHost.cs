var builder = DistributedApplication.CreateBuilder(args);

var mssql = builder.AddSqlServer("sqlserver");
var mssqlDb = mssql.AddDatabase("AirlineDb");

var apiHost = builder.AddProject<Projects.AirlineApp_Api>("AirlineAppAPI")
    .WithReference(mssqlDb, "DefaultConnection")
    .WaitFor(mssqlDb);

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

var rabbitUserName = builder.AddParameter("RabbitMQLogin");
var rabbitPassword = builder.AddParameter("RabbitMQPassword");
var rabbitMq = builder.AddRabbitMQ("airlineapp-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
    .WithManagementPlugin();

var rabbiMqQueue = builder.AddParameter("RabbitMQQueue");
builder.AddProject<Projects.AirlineApp_Generator_RabbitMq_Host>("airlineapp-generator-rabbitmq-host")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:PayloadLimit", payloadLimit)
    .WithEnvironment("Generator:WaitTime", waitTime)
    .WithEnvironment("RabbitMq:QueueName", rabbiMqQueue);

apiHost.WithEnvironment("RabbitMq:QueueName", rabbiMqQueue)
    .WithReference(rabbitMq);

builder.Build().Run();

