using System;
using Microsoft.AspNetCore.Mvc;
using LocationReporter.Events;
using LocationReporter.Models;
using LocationReporter.Services;

namespace LocationReporter.Controllers
{
    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private readonly ICommandEventConverter converter;
        private readonly IEventEmitter eventEmitter;
        private readonly ITeamServiceClient teamServiceClient;
        

        public LocationReportsController(ICommandEventConverter converter, 
            IEventEmitter eventEmitter, 
            ITeamServiceClient teamServiceClient) {
            this.converter = converter;
            this.eventEmitter = eventEmitter;
            this.teamServiceClient = teamServiceClient;
        }

        [HttpPost]
        public ActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
        {
            var locationRecordedEvent = this.converter.CommandToEvent(locationReport);
            locationRecordedEvent.TeamID = this.teamServiceClient.GetTeamForMember(locationReport.MemberId);
            this.eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportId}", locationReport);
        }
    }
}