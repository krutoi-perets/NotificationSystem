using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using NotificationSystem;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                      x => x.MigrationsAssembly("NotificationSystem"));
});
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<RabbitMqPublisher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.MapSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
