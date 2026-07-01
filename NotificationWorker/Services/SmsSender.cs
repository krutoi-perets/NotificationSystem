using Shared;

namespace NotificationWorker
{
    public class SmsSender
    {
        public async Task Send(Notification notification)
        {
            Console.WriteLine($"Сообщение {notification.Id} отправлено по SMS");
            await Task.Delay(1000);
        }
    }
}
