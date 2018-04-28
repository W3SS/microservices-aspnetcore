using System;

namespace StatlerWaldorfCorp.EventProcessor.Location
{
    using global::EventProcessor.Location;

    public class GpsUtility 
    {
        private const double C_EARTH = 40000.0;

        public double DegToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /*
         * Returns the distance between two GPS coordinates in kilometers.         
         */
        public double DistanceBetweenPoints(GpsCoordinate point1, GpsCoordinate point2)
        {            
            var distance = 0.0;
        
        
            var lat1Rad = DegToRad(point1.Latitude);
            var long1Rad = DegToRad(point1.Longitude);
            var lat2Rad = DegToRad(point2.Latitude);
            var long2Rad = DegToRad(point2.Longitude);

            var longDiff = Math.Abs(long1Rad - long2Rad);

            if (longDiff > Math.PI)
            {
                longDiff = 2.0 * Math.PI - longDiff;
            }
        
            var angleCalculation =
                Math.Acos(
                    Math.Sin(lat2Rad) * Math.Sin(lat1Rad) +
                    Math.Cos(lat2Rad) * Math.Cos(lat1Rad) * Math.Cos(longDiff));

            distance = C_EARTH * angleCalculation / (2.0 * Math.PI);

            return distance;
        }
    }
}