using System;
using LocationReporter.Events;

namespace LocationReporter.Models
{
    public class CommandEventConverter : ICommandEventConverter
    {
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport) 
        {
            var locationRecordedEvent = new MemberLocationRecordedEvent {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberID = locationReport.MemberId,
                ReportID = locationReport.ReportId,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };

           return locationRecordedEvent;
        }
    }
}