namespace Shared
{
    public class SmtpServer
    {
        public int Id { get; set; }
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Priority { get; set; }
        public bool IsActive { get; set; }
    }
}
