using System.IO;
using System.Threading;
using Grpc.Core;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PartialFoods.Services.InventoryServer.Entities;
using Grpc.Reflection.V1Alpha;
using Grpc.Reflection;

namespace PartialFoods.Services.InventoryServer
{
    class Program
    {
        private static readonly ManualResetEvent mre = new ManualResetEvent(false);

        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables();

            Configuration = builder.Build();

            var loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            ILogger logger = loggerFactory.CreateLogger<Program>();
            var rpcLogger = loggerFactory.CreateLogger<InventoryManagementImpl>();

            var port = int.Parse(Configuration["service:port"]);

            var brokerList = Configuration["kafkaclient:brokerlist"];
            const string ReservedTopic = "inventoryreserved";
            const string ReleasedTopic = "inventoryreleased";

            var config = new Dictionary<string, object>
            {
                { "group.id", "inventory-server" },
                { "enable.auto.commit", false },
                { "bootstrap.servers", brokerList }
            };
            //var context = new InventoryContext(Configuration["postgres:connectionstring"]);
            var context = new InventoryContext();
            var repo = new InventoryRepository(context);

            var reservedEventProcessor = new InventoryReservedEventProcessor(repo);
            var kafkaConsumer = new KafkaReservedConsumer(ReservedTopic, config, reservedEventProcessor);
            kafkaConsumer.Consume();

            var releasedEventProcessor = new InventoryReleasedEventProcessor(repo);
            var releasedConsumer = new KafkaReleasedConsumer(ReleasedTopic, config, releasedEventProcessor);
            releasedConsumer.Consume();

            var refImpl = new ReflectionServiceImpl(
                ServerReflection.Descriptor, InventoryManagement.Descriptor);
            var inventoryManagement = new InventoryManagementImpl(repo, rpcLogger);
            var server = new Server
            {
                Services = { InventoryManagement.BindService(inventoryManagement),
                             ServerReflection.BindService(refImpl)},
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();
            logger.LogInformation("Inventory gRPC Service Listening on Port " + port);

            mre.WaitOne();
        }
    }
}
