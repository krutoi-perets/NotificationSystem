using Shared;

namespace NotificationWorker
{
    public class EmailSender
    {
        public async Task Send(Notification notification)
        {
            Console.WriteLine($"Сообщение {notification.Id} отправлено по Email");
            await Task.Delay(1000);
        }
    }
}
