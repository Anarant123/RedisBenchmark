using RedisBenchmark.CLI.Parts;
using StackExchange.Redis;
using System.Reactive.Linq;

const string connectionString = "redis-server:6379";
const string channel = "benchmark_channel";

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

Console.WriteLine("Нажмите любую клавишу для завершения...");
Console.ReadKey();