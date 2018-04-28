using System;
using Microsoft.Extensions.Options;
using PubnubApi;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ProximityMonitor.Realtime
{
    public class PubnubFactory
    {     
        private readonly PNConfiguration pnConfiguration;

        private ILogger logger;
        
        public PubnubFactory(IOptions<PubnubOptions> pubnubOptions,
            ILogger<PubnubFactory> logger)
        {
            this.logger = logger;

            this.pnConfiguration = new PNConfiguration
                                       {
                                           PublishKey = pubnubOptions.Value.PublishKey,
                                           SubscribeKey = pubnubOptions.Value.SubscribeKey,
                                           Secure = false
                                       };

            logger.LogInformation($"Pubnub Factory using publish key {this.pnConfiguration.PublishKey}");
        }

        public Pubnub CreateInstance()
        {
            return new Pubnub(this.pnConfiguration);    
        }
    }
}