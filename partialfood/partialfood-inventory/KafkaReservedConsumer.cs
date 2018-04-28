using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;

namespace PartialFoods.Services.InventoryServer
{

    public class KafkaReservedConsumer
    {
        private readonly string topic;
        private readonly Dictionary<string, object> config;
        private readonly InventoryReservedEventProcessor eventProcessor;

        public KafkaReservedConsumer(string topic, Dictionary<string, object> config, InventoryReservedEventProcessor eventProcessor)
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
                    //consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, 0) });
                    consumer.Subscribe(new[] { this.topic });

                    while (true)
                    {
                        Message<Null, string> msg;
                        if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                        {
                            var rawJson = msg.Value;
                            try
                            {
                                var evt = JsonConvert.DeserializeObject<InventoryReservedEvent>(rawJson);
                                this.eventProcessor.HandleInventoryReservedEvent(evt);
                                var committedOffsets = consumer.CommitAsync(msg).Result;
                                if (committedOffsets.Error.HasError)
                                {
                                    Console.WriteLine($"Failed to commit offsets : {committedOffsets.Error.Reason}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                                Console.WriteLine($"Failed to handle inventory reserved event : ${ex}");
                            }
                        }
                    }
                }
            });
        }
    }
}