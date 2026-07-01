using Microsoft.EntityFrameworkCore;
using NotificationWorker;
using Shared;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<RabbitMqConsumer>();
builder.Services.AddSingleton<EmailSender>();
builder.Services.AddSingleton<SmsSender>();
builder.Services.AddSingleton<PushSender>();

var host = builder.Build();
host.Run();
