using RedisBenchmark.CLI.Parts;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory()) 
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
  .Build();

var connectionString = config["ConnectionString"];
const string channel = "benchmark_channel";

if (connectionString != null)
{
  var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);

  var publisher = new Publisher(redis, channel);

  var subscriber1 = new Subscriber(redis, channel, "Subscriber1");
  var subscriber2 = new Subscriber(redis, channel, "Subscriber2");
  var subscriber3 = new Subscriber(redis, channel, "Subscriber3");
  var subscriber4 = new Subscriber(redis, channel, "Subscriber4");
  var subscriber5 = new Subscriber(redis, channel, "Subscriber5");

  for (var i = 1; i <= 5; i++)
  {
    await publisher.PublishMessagesAsync($"Сообщение {i}");
    await Task.Delay(5000);
  }
}

Console.WriteLine("Нажмите любую клавишу для завершения...");
Console.ReadKey();