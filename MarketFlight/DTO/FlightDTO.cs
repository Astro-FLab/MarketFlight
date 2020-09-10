using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.DTO
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public int DepartureAirportId { get; set; }
        public string DepartureAirportName { get; set; }
        public int ArrivalAirportId { get; set; }
        public string ArrivalAirportName { get; set; }
    }
}
