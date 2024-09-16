using System.Diagnostics;
using StackExchange.Redis;

namespace RedisBenchmark.CLI.Parts;

public class Publisher
{
  private readonly ISubscriber p_subscriber;

  public Publisher(IConnectionMultiplexer _redis)
  {
    p_subscriber = _redis.GetSubscriber();
  }

  public async Task PublishMessagesAsync(string _channel, int _messageCount)
  {
    for (var i = 0; i < _messageCount; i++)
    {
      var timestamp = Stopwatch.GetTimestamp();
      await p_subscriber.PublishAsync(_channel, timestamp.ToString());
    }
  }

  
}