namespace locationservice.Controllers {
    using System;

    using locationservice.Models;

    using Microsoft.AspNetCore.Mvc;

    [Route("locations/{memberId}")]
    public class LocationRecordController : Controller {

        private readonly ILocationRecordRepository locationRepository;

        public LocationRecordController(ILocationRecordRepository repository) {
            this.locationRepository = repository;
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId, [FromBody]LocationRecord locationRecord) {
            this.locationRepository.Add(locationRecord);
            return this.Created($"/locations/{memberId}/{locationRecord.Id}", locationRecord);
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId) {            
            return this.Ok(this.locationRepository.AllForMember(memberId));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberId) {
            return this.Ok(this.locationRepository.GetLatestForMember(memberId));
        }
    }
}