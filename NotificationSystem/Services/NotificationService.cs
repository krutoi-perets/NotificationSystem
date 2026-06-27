namespace NotificationSystem
{
    public class NotificationService
    {
        private readonly ApplicationContext _context;

        public NotificationService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Notification? Get(int id)
        {
            return _context.Notifications.Find(id);
        }

        public Notification Create(CreateNotificationDTO dto)
        {
            Notification notification = new Notification
            {
                Channel = dto.Channel,
                Receiver = dto.Receiver,
                Content = dto.Content,
                CreatedAt = DateTime.Now,
                Status = NotificationStatus.Pending
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            return notification;
        }
    }
}