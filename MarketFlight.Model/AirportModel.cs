using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.Model
{
    public class AirportModel
    {
        public AirportModel( int airportId, string name )
        {
            AirportId = airportId;
            Name = name;
        }

        public int AirportId { get; set; }
        public string Name { get; set; }
    }
}
