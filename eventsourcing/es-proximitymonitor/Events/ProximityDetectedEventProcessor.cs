using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProximityMonitor.Queues;
using ProximityMonitor.Realtime;
using ProximityMonitor.TeamService;

namespace ProximityMonitor.Events
{
    public class ProximityDetectedEventProcessor : IEventProcessor
    {
        private ILogger logger;
        private IRealtimePublisher publisher;
        private readonly IEventSubscriber subscriber;

        private readonly PubnubOptions pubnubOptions;

        public ProximityDetectedEventProcessor(
            ILogger<ProximityDetectedEventProcessor> logger,
            IRealtimePublisher publisher,
            IEventSubscriber subscriber,
            ITeamServiceClient teamClient,
            IOptions<PubnubOptions> pubnubOptions)
        {
            this.logger = logger;
            this.pubnubOptions = pubnubOptions.Value;
            this.publisher = publisher;
            this.subscriber = subscriber;            

            logger.LogInformation("Created Proximity Event Processor.");        

            subscriber.ProximityDetectedEventReceived += (pde) => {
                var t = teamClient.GetTeam(pde.TeamID);
                var sourceMember = teamClient.GetMember(pde.TeamID, pde.SourceMemberID);
                var targetMember = teamClient.GetMember(pde.TeamID, pde.TargetMemberID);

                var outEvent = new ProximityDetectedRealtimeEvent 
                {
                    TargetMemberID = pde.TargetMemberID,
                    SourceMemberID = pde.SourceMemberID,
                    DetectionTime = pde.DetectionTime,                    
                    SourceMemberLocation = pde.SourceMemberLocation,
                    TargetMemberLocation = pde.TargetMemberLocation,
                    MemberDistance = pde.MemberDistance,
                    TeamID = pde.TeamID,
                    TeamName = t.Name,
                    SourceMemberName = $"{sourceMember.FirstName} {sourceMember.LastName}",
                    TargetMemberName = $"{targetMember.FirstName} {targetMember.LastName}"
                };
                publisher.Publish(this.pubnubOptions.ProximityEventChannel, outEvent.toJson());
            };            
        }    
        
        public void Start()
        {
            this.subscriber.Subscribe();
        }

        public void Stop()
        {
            subscriber.Unsubscribe();
        }
    }
}