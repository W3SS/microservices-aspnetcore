using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;

namespace PartialFoods.Services.OrderManagementServer
{
    public class KafkaOrdersConsumer
    {
        private readonly string topic;
        private readonly Dictionary<string, object> config;
        private readonly OrderAcceptedEventProcessor eventProcessor;

        public KafkaOrdersConsumer(string topic, Dictionary<string, object> config, OrderAcceptedEventProcessor eventProcessor)
        {
            this.topic = topic;
            this.config = config;
            this.eventProcessor = eventProcessor;
        }

        public void Consume()
        {
            Task.Run(() =>
            {
                Console.WriteLine($"Starting Kafka subscription to {this.topic}");
                using (var consumer = new Consumer<Null, string>(this.config, null, new StringDeserializer(Encoding.UTF8)))
                {

                    consumer.OnError += (_, error) =>
                       Console.WriteLine($"Orders Error: {error}");

                    consumer.OnConsumeError += (_, error) =>
                        Console.WriteLine($"Orders Consume Error: {error}");

                    consumer.OnMessage += (_, msg) =>
                        Console.WriteLine($"Orders message available: {msg}");

                    consumer.Subscribe(new[] { this.topic });
                    consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(this.topic, 0, 0) });
                    //consumer.Seek(new TopicPartitionOffset(topic, 0, 0));
                    while (true)
                    {
                        Message<Null, string> msg;
                        if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                        {
                            Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                            string rawJson = msg.Value;
                            try
                            {
                                OrderAcceptedEvent evt = JsonConvert.DeserializeObject<OrderAcceptedEvent>(rawJson);
                                this.eventProcessor.HandleOrderAcceptedEvent(evt);
                                var committedOffsets = consumer.CommitAsync(msg).Result;
                                if (committedOffsets.Error.HasError)
                                {
                                    Console.WriteLine($"Failed to commit offsets : {committedOffsets.Error.Reason}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                Console.WriteLine($"Failed to handle order accepted event : ${ex}");
                            }
                        }
                    }
                }
            });

        }
    }
}