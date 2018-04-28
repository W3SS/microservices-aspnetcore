using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace EventProcessor.Events
{
    using EventProcessor.Location;
    using EventProcessor.Queues;

    public class MemberLocationEventProcessor : IEventProcessor
    {
        private ILogger logger;
        private readonly IEventSubscriber subscriber;

        private IEventEmitter eventEmitter;

        private readonly ProximityDetector proximityDetector;

        private ILocationCache locationCache;

        public MemberLocationEventProcessor(
            ILogger<MemberLocationEventProcessor> logger,
            IEventSubscriber eventSubscriber,
            IEventEmitter eventEmitter,
            ILocationCache locationCache
        )
        {
            this.logger = logger;
            this.subscriber = eventSubscriber;
            this.eventEmitter = eventEmitter;
            this.proximityDetector = new ProximityDetector();
            this.locationCache = locationCache;

            this.subscriber.MemberLocationRecordedEventReceived += (mlre) => {

                var memberLocations = locationCache.GetMemberLocations(mlre.TeamId);
                var proximityEvents = 
                    proximityDetector.DetectProximityEvents(mlre, memberLocations, 30.0f);
                foreach (var proximityEvent in proximityEvents) {
                    eventEmitter.EmitProximityDetectedEvent(proximityEvent);
                }

                locationCache.Put(mlre.TeamId, new MemberLocation { MemberId = mlre.MemberId, Location = new GpsCoordinate {
                    Latitude = mlre.Latitude, Longitude = mlre.Longitude
                } });
            };
        }       

        public void Start()
        {
            this.subscriber.Subscribe();
        }

        public void Stop()
        {
            this.subscriber.Unsubscribe();
        }
    }
}