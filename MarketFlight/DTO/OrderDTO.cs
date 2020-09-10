using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int SeatCount { get; set; }
        public int FlightId { get; set; }
    }
}
