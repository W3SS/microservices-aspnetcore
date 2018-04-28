using System;

namespace LocationReporter.Models
{
    public class LocationReport
    {
        public Guid ReportId { get; set; }
        public string Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberId { get; set; }
    }
}
