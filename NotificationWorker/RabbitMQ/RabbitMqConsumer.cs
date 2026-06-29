using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationWorker
{
    public class RabbitMqConsumer
    {
        public async Task StartAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "notifications",
                durable: false,
                exclusive: false,
                autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Получено уведомление: {message}");

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "notifications",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine("Worker запущен");

            await Task.Delay(Timeout.Infinite);
        }
    }
}
