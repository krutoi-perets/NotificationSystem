using System.Text.Json.Serialization;

namespace Shared
{
    public class Notification
    {
        public int Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationChannel Channel { get; set; }
        public string Receiver { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotificationStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
