using StackExchange.Redis;
using System;

namespace RedisBenchmark.CLI.Parts;

public class Publisher
{
  private readonly ISubscriber p_subscriber;
  private readonly string p_channelName;

  public Publisher(IConnectionMultiplexer _redis, string _channelName)
  {
    p_channelName = _channelName;
    p_subscriber = _redis.GetSubscriber();
  }

  public async Task PublishMessagesAsync(string _message)
  {
    var messageWithTimestamp = $"{_message}|{DateTime.UtcNow:O}";
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"{DateTime.UtcNow}| Publisher отправил сообщение: {messageWithTimestamp}");
    Console.ForegroundColor = ConsoleColor.White;
    await p_subscriber.PublishAsync(p_channelName, messageWithTimestamp);
  }
  
  public void PublishMessages(string _message)
  {
    var messageWithTimestamp = $"{_message}|{DateTime.UtcNow:O}";
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"{DateTime.UtcNow}| Publisher отправил сообщение: {messageWithTimestamp}");
    Console.ForegroundColor = ConsoleColor.White;
    p_subscriber.Publish(p_channelName, messageWithTimestamp);
  }
}