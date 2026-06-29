using NotificationWorker;

var consumer = new RabbitMqConsumer();

await consumer.StartAsync();