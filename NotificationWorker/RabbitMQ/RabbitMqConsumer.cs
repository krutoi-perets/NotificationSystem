using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;

namespace NotificationWorker
{
    public class RabbitMqConsumer
    {
        private IConnection? _connection;
        private IChannel? _channel;

        public async Task StartAsync(Func<int, Task> func, CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync("notifications", false, false, false);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, e) =>
            {
                var id = int.Parse(Encoding.UTF8.GetString(e.Body.ToArray()));

                await func(id);
            };

            await _channel.BasicConsumeAsync("notifications", true, consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
