using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.Model
{
    public class OrderModel
    {
        public OrderModel( int orderId, int userId, DateTime orderDate, int seatCount, int flightId, int departureAirportId, string departureAirportName, int arrivalAirportId, string arrivalAirportName )
        {
            OrderId = orderId;
            UserId = userId;
            OrderDate = orderDate;
            SeatCount = seatCount;
            FlightId = flightId;
            DepartureAirportId = departureAirportId;
            DepartureAirportName = departureAirportName;
            ArrivalAirportId = arrivalAirportId;
            ArrivalAirportName = arrivalAirportName;
        }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int SeatCount { get; set; }
        public int FlightId { get; set; }
        public int DepartureAirportId { get; set; }
        public string DepartureAirportName { get; set; }
        public int ArrivalAirportId { get; set; }
        public string ArrivalAirportName { get; set; }
    }
}
