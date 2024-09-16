using System.Diagnostics;
using StackExchange.Redis;

namespace RedisBenchmark.CLI.Parts;

public class Subscriber
{
  private readonly ISubscriber p_subscriber;
  private int p_messageReceived;

  public Subscriber(IConnectionMultiplexer _redis)
  {
    p_subscriber = _redis.GetSubscriber();
  }

  public async Task SubscribeAsync(string _channel, int _expectedMessageCount)
  {
    p_messageReceived = 0;
    List<double> latencies = new List<double>();

    await p_subscriber.SubscribeAsync(_channel, (_chan, _message) =>
    {
      p_messageReceived++;

      var sentTimestamp = long.Parse(_message);
      var receivedTimestamp = Stopwatch.GetTimestamp();
      var latency = (receivedTimestamp - sentTimestamp) * 1000.0 / Stopwatch.Frequency;
      latencies.Add(latency);

      if (p_messageReceived % 1000 == 0)
      {
        Console.WriteLine($"{p_messageReceived} сообщений получено, средняя задержка: {latencies.Average():F2} мс");
        latencies.Clear();
      }

      if (p_messageReceived != _expectedMessageCount) return;
      Console.WriteLine("Все сообщения получены.");
      Console.WriteLine($"Средняя задержка: {latencies.Average():F2} мс");
    });
  }
}