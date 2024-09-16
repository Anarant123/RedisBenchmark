using RedisBenchmark.CLI.Parts;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory()) 
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
  .Build();

var connectionString = config["ConnectionString"];
var subscribersCount = int.Parse(config["SubscribersCount"]);
const string channel = "benchmark_channel";

var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);

var publisher = new Publisher(redis, channel);

var subscribers = new List<Subscriber>(subscribersCount);

for (var i = 1; i <= subscribersCount; i++)
{
  subscribers.Add(new Subscriber(redis, channel, $"Subscriber{i}"));
}

for (var i = 1; i <= 5; i++)
{
  await publisher.PublishMessagesAsync($"Сообщение {i}");
  await Task.Delay(5000);
}

Console.WriteLine("Нажмите любую клавишу для завершения...");
Console.ReadKey();