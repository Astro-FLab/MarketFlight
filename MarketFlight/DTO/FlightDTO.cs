namespace MarketFlight.DTO
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int TotalSeatCount { get; set; }
    }
}
