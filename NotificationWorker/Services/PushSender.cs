using Shared;

namespace NotificationWorker
{
    public class PushSender
    {
        public async Task Send(Notification notification)
        {
            Console.WriteLine($"Сообщение {notification.Id} отправлено по Push");
            await Task.Delay(1000);
        }
    }
}
