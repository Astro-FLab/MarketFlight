using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.Model
{
    public class BundleModel
    {
        public int BundleId { get; set; }
        public IEnumerable<FlightModel> Flights { get; set; }
    }
}
