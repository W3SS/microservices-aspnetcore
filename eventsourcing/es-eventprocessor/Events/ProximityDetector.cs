using System.Collections.Generic;
using StatlerWaldorfCorp.EventProcessor.Location;
using System.Linq;
using System;

namespace EventProcessor.Events
{
    using EventProcessor.Location;

    public class ProximityDetector
    {
        /*
         * This method assumes that the memberLocations collection only 
         * applies to members applicable for proximity detection. In other words,
         * non-team-mates must be filtered out before using this method.
         * distance threshold is in Kilometers.
         */
        public ICollection<ProximityDetectedEvent> DetectProximityEvents(
            MemberLocationRecordedEvent memberLocationEvent,
            ICollection<MemberLocation> memberLocations,
            double distanceThreshold)
        {
            var gpsUtility = new GpsUtility();
            var sourceCoordinate = new GpsCoordinate {
                Latitude = memberLocationEvent.Latitude,
                Longitude = memberLocationEvent.Longitude
            };
       
            return memberLocations.Where( 
                      ml => ml.MemberId != memberLocationEvent.MemberId &&                     
                      gpsUtility.DistanceBetweenPoints(sourceCoordinate, ml.Location) < distanceThreshold)            
                .Select( ml => new ProximityDetectedEvent {
                                                                    SourceMemberId = memberLocationEvent.MemberId,
                                                                    TargetMemberId = ml.MemberId,
                                                                    TeamId = memberLocationEvent.TeamId,
                                                                    DetectionTime = DateTime.UtcNow.Ticks,
                                                                    SourceMemberLocation = sourceCoordinate,
                                                                    TargetMemberLocation = ml.Location,
                                                                    MemberDistance = gpsUtility.DistanceBetweenPoints(sourceCoordinate, ml.Location)
                                                                }).ToList();                            
        }
    }
}