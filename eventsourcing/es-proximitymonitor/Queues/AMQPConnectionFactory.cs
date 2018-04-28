using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace ProximityMonitor.Queues
{
    using System;

    public class AMQPConnectionFactory : ConnectionFactory
    {
        protected AMQPOptions amqpOptions;

        public AMQPConnectionFactory(
            ILogger<AMQPConnectionFactory> logger,
            IOptions<AMQPOptions> serviceOptions)
        {
            this.amqpOptions = serviceOptions.Value;

            this.UserName = this.amqpOptions.Username;
            this.Password = this.amqpOptions.Password;
            this.VirtualHost = this.amqpOptions.VirtualHost;
            this.HostName = this.amqpOptions.HostName;
            this.Uri = new Uri(this.amqpOptions.Uri);

            logger.LogInformation($"AMQP Connection configured for URI : {this.amqpOptions.Uri}");
        }
    }
}