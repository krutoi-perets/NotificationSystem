using Microsoft.Extensions.Logging.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System.Text;

namespace NotificationWorker
{
    public class Worker(ILogger<Worker> logger,
                        IServiceScopeFactory scopeFactory,
                        RabbitMqConsumer consumer,
                        EmailSender emailSender,
                        SmsSender smsSender,
                        PushSender pushSender) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Worker запущен");

            try
            {
                await consumer.StartAsync(ProcessNotification, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Worker упал");
            }
        }

        private async Task ProcessNotification(int id)
        {
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var notification = await context.Notifications.FindAsync(id);

            if (notification == null) return;

            logger.LogInformation($"Получено уведомление {id}");
            notification.Status = NotificationStatus.Processing;
            await context.SaveChangesAsync();

            try
            {
                logger.LogInformation($"Отправка уведомления {id}");
                switch(notification.Channel)
                {
                    case NotificationChannel.Email:
                        await emailSender.Send(notification);
                        break;
                    case NotificationChannel.SMS:
                        await smsSender.Send(notification);
                        break;
                    case NotificationChannel.Push:
                        await pushSender.Send(notification);
                        break;

                    default:
                        throw new Exception($"Неизвестный канал: {notification.Channel}");
                }

                logger.LogInformation($"Уведомление {id} отправлено");
                notification.Status = NotificationStatus.Sent;
                notification.SentAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Уведомление {id} не отправлено. Ошибка: {ex.Message}");
                notification.Status = NotificationStatus.Failed;
                notification.ErrorMessage = ex.Message;
            }
            
            await context.SaveChangesAsync();
        }
    }
}
