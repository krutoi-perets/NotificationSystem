namespace NotificationSystem
{
    public class CreateNotificationDTO
    {
        public NotificationChannel Channel { get; set; }
        public string Receiver { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
