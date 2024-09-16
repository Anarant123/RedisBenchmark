using StackExchange.Redis;
using System;

namespace RedisBenchmark.CLI.Parts;

public class Subscriber
{
  public Subscriber(IConnectionMultiplexer _redis, string _channelName, string _subscriberName)
  {
    var subscriberName = _subscriberName;
    var subscriber = _redis.GetSubscriber();
    subscriber.SubscribeAsync(_channelName, (_channel, _message) =>
    {
      var messageParts = _message.ToString().Split('|');
      if (messageParts.Length == 2 && DateTime.TryParse(messageParts[1], out var sentTime))
      {
        var receivedTime = DateTime.UtcNow;
        var timeDifference = receivedTime - sentTime;
        Console.WriteLine($"{subscriberName} получил сообщение: {messageParts[0]} в {receivedTime:O}, " +
                          $"задержка: {timeDifference.TotalMilliseconds} мс");
      }
      else
      {
        Console.WriteLine($"{subscriberName} получил сообщение: {_message}");
      }
    });
  }
}