using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.Model
{
    public class FlightModel
    {
        public FlightModel( int flightId, int departureAirportId, string departureAirportName, int arrivalAirportId, string arrivalAirportName )
        {
            FlightId = flightId;
            DepartureAirportId = departureAirportId;
            DepartureAirportName = departureAirportName;
            ArrivalAirportId = arrivalAirportId;
            ArrivalAirportName = arrivalAirportName;
        }

        public int FlightId { get; set; }
        public int DepartureAirportId { get; set; }
        public string DepartureAirportName { get; set; }
        public int ArrivalAirportId { get; set; }
        public string ArrivalAirportName { get; set; }
    }
}
