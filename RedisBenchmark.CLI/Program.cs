using System.Diagnostics;
using RedisBenchmark.CLI.Parts;
using StackExchange.Redis;

namespace RedisBenchmark.CLI;

class Program
{
  static async Task Main(string[] _args)
  {
    const string connectionString = "redis-server:6379";
    const string channel = "benchmark_channel";
    const int messageCount = 10000;

    var redis = await ConnectionMultiplexer.ConnectAsync(connectionString);

    var subscriber = new Subscriber(redis);
    var publisher = new Publisher(redis);

    var subscribeTask = subscriber.SubscribeAsync(channel, messageCount);

    await Task.Delay(500);

    var stopwatch = Stopwatch.StartNew();
    await publisher.PublishMessagesAsync(channel, messageCount);
    stopwatch.Stop();

    Console.WriteLine($"Опубликовано {messageCount} сообщений за {stopwatch.ElapsedMilliseconds} мс");

    await subscribeTask;

    Console.WriteLine("Бенчмарк завершен.");
  }
}