using Grpc.Core;

using System.Collections.Generic;

using PartialFoods.Services.OrderManagementServer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;

namespace PartialFoods.Services.OrderManagementServer
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static readonly ManualResetEvent mre = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables();


            Configuration = builder.Build();

            Microsoft.Extensions.Logging.ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();

            string brokerList = Configuration["kafkaclient:brokerlist"];
            const string Topic = "orders";
            const string CanceledTopic = "canceledorders";

            var config = new Dictionary<string, object>
            {
                { "group.id", "order-management" },
                { "enable.auto.commit", false },
                { "bootstrap.servers", brokerList }
            };

            var context = new OrdersContext(Configuration["postgres:connectionstring"]);
            var repo = new OrderRepository(context);
            var eventProcessor = new OrderAcceptedEventProcessor(repo);
            var canceledProcessor = new OrderCanceledEventProcessor(repo);

            var orderConsumer = new KafkaOrdersConsumer(Topic, config, eventProcessor);
            var activityConsumer = new KafkaActivitiesConsumer(CanceledTopic, config, canceledProcessor);
            orderConsumer.Consume();
            activityConsumer.Consume();

            var port = int.Parse(Configuration["service:port"]);

            var refImpl = new ReflectionServiceImpl(
                ServerReflection.Descriptor, OrderManagement.Descriptor);
            Server server = new Server
            {
                Services = { OrderManagement.BindService(new OrderManagementImpl(repo)),
                             ServerReflection.BindService(refImpl) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();
            logger.LogInformation("Order management gRPC service listening on port " + port);

            mre.WaitOne();

        }
    }
}
