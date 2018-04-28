using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using LocationReporter.Models;

namespace LocationReporter.Events
{
    using System;

    // ReSharper disable once InconsistentNaming
    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger logger;

        private readonly AMQPOptions rabbitOptions;

        private readonly ConnectionFactory connectionFactory;

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger,
            IOptions<AMQPOptions> amqpOptions)
        {
            this.logger = logger;
            this.rabbitOptions = amqpOptions.Value;

            this.connectionFactory = new ConnectionFactory
                                    {
                                        UserName = this.rabbitOptions.Username,
                                        Password = this.rabbitOptions.Password,
                                        VirtualHost = this.rabbitOptions.VirtualHost,
                                        HostName = this.rabbitOptions.HostName,
                                        Uri = new Uri(this.rabbitOptions.Uri)
                                    };


            logger.LogInformation("AMQP Event Emitter configured with URI {0}", this.rabbitOptions.Uri);
        }
        public const string QueueLocationrecorded = "memberlocationrecorded";

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {                    
            using (IConnection conn = this.connectionFactory.CreateConnection()) {
                using (IModel channel = conn.CreateModel()) {
                    channel.QueueDeclare(
                        queue: QueueLocationrecorded,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );
                    string jsonPayload = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QueueLocationrecorded,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}