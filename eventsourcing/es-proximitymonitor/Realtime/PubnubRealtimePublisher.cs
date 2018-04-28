using Microsoft.Extensions.Logging;
using PubnubApi;


namespace ProximityMonitor.Realtime
{
    public class PubnubRealtimePublisher : IRealtimePublisher
    {
        private readonly ILogger logger;

        private readonly Pubnub pubnubClient;

        public PubnubRealtimePublisher(
            ILogger<PubnubRealtimePublisher> logger,
            Pubnub pubnubClient)
        {
            logger.LogInformation("Realtime Publisher (Pubnub) Created.");
            this.logger = logger;
            this.pubnubClient = pubnubClient;            
        }

        public void Validate()        
        {
            this.pubnubClient.Time()
                .Async(new PNTimeResultExt(
                (result, status) => {
                    if (status.Error) {
                        this.logger.LogError($"Unable to connect to Pubnub {status.ErrorData.Information}");
                        throw status.ErrorData.Throwable;
                    } else {
                        this.logger.LogInformation("Pubnub connection established.");
                    }
                }
            ));        

        }

        public void Publish(string channelName, string message)
        {
            this.pubnubClient.Publish()
                .Channel(channelName)
                .Message(message)
                .Async(new PNPublishResultExt(
                    (result, status) => {
                        if (status.Error) {
                            this.logger.LogError($"Failed to publish on channel {channelName}: {status.ErrorData.Information}");
                        } else {
                            this.logger.LogInformation($"Published message on channel {channelName}, {status.AffectedChannels.Count} affected channels, code: {status.StatusCode}");
                        }                        
                    }
                ));
        }
    }
}