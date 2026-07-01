using Microsoft.Extensions.Validation;
using RabbitMQ.Client;
using System.Text;

namespace NotificationSystem
{
    public class RabbitMqPublisher
    {
        public async Task PublishAsync(int id)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync("notifications", false, false, false);

            var body = Encoding.UTF8.GetBytes(id.ToString());

            await channel.BasicPublishAsync("", "notifications", body);
        }
    }
}
