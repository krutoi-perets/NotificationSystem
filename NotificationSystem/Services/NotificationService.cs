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

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<List<Notification>> GetByReceiverAsync(string receiver)
        {
            return await _context.Notifications.Where(x => x.Receiver == receiver).ToListAsync();
        }

        public async Task<List<Notification>> GetByChannelAsync(NotificationChannel channel)
        {
            return await _context.Notifications.Where(x => x.Channel == channel).ToListAsync();
        }

        public async Task<List<Notification>> GetByDateAsync(DateTime date)
        {
            return await _context.Notifications.Where(x => x.CreatedAt.Date == date).ToListAsync();
        }

        public async Task<Notification> CreateAsync(CreateNotificationDTO dto)
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