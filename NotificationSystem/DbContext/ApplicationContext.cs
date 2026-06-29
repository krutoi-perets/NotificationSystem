using Microsoft.EntityFrameworkCore;
using Shared;

namespace NotificationSystem
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<SmtpServer> SmtpServers { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) { }
    }
}
