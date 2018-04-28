using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ProximityMonitor.Events;

namespace ProximityMonitor.Queues
{
    // ReSharper disable once InconsistentNaming
    public class RabbitMQEventSubscriber : IEventSubscriber
    {
        public event ProximityDetectedEventReceivedDelegate ProximityDetectedEventReceived;

        private IConnectionFactory connectionFactory;
        private readonly QueueOptions queueOptions;
        private readonly EventingBasicConsumer consumer;   
        private readonly IModel channel;   
        private string consumerTag;  
        private readonly ILogger logger;

        public RabbitMQEventSubscriber(ILogger<RabbitMQEventSubscriber> logger,
            IConnectionFactory connectionFactory,
            IOptions<QueueOptions> queueOptions,
            EventingBasicConsumer consumer)
        {
            this.connectionFactory = connectionFactory;
            this.queueOptions = queueOptions.Value;
            this.consumer = consumer;
            this.channel = consumer.Model;
            this.logger = logger;

            logger.LogInformation("Created RabbitMQ event subscriber.");
            this.Initialize();
        }

        private void Initialize()
        {
            this.channel.QueueDeclare(
                queue: this.queueOptions.ProximityDetectedEventQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            this.consumer.Received += (ch, ea) => {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body);
                var evt = JsonConvert.DeserializeObject<ProximityDetectedEvent>(msg);
                    this.logger.LogInformation($"Received incoming event, {body.Length} bytes.");
                    this.ProximityDetectedEventReceived?.Invoke(evt);
                    this.channel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public void Subscribe()
        {
            this.consumerTag = channel.BasicConsume(this.queueOptions.ProximityDetectedEventQueueName, false, this.consumer);
            this.logger.LogInformation($"Subscribed to queue {this.queueOptions.ProximityDetectedEventQueueName}, ctag = {this.consumerTag}");
        }

        public void Unsubscribe()
        {
            this.channel.BasicCancel(this.consumerTag);
            this.logger.LogInformation($"Stopped subscription on queue {this.queueOptions.ProximityDetectedEventQueueName}");
        }
    }
}