using Microsoft.EntityFrameworkCore;
using Shared;

namespace NotificationSystem
{
    public class NotificationService
    {
        private readonly ApplicationContext _context;
        private readonly RabbitMqPublisher _publisher;

        public NotificationService(ApplicationContext context, RabbitMqPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification?> Get(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<Notification> Create(CreateNotificationDTO dto)
        {
            Notification notification = new Notification
            {
                Channel = dto.Channel,
                Receiver = dto.Receiver,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                Status = NotificationStatus.Pending
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            await _publisher.PublishAsync(notification.Id);

            return notification;
        }
    }
}