namespace NotificationSystem
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationChannel Channel { get; set; }
        public string Receiver { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime SentAt { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
