using Microsoft.EntityFrameworkCore;

namespace NotificationSystem
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) { }
    }
}
