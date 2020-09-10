using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CK.Core;
using MarketFlight.Data;
using MarketFlight.DTO;
using MarketFlight.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarketFlight.Controllers
{
    [ApiController]
    [Route( "marketflight/flights" )]
    public class FlightController : ControllerBase
    {
        readonly IActivityMonitor _m;
        private readonly IDbConnection _dbConnection;

        public FlightController( IActivityMonitor m, IDbConnection dbConnection )
        {
            _m = m;
            _dbConnection = dbConnection;
        }
        [HttpGet]
        public Task<IEnumerable<FlightModel>> GetAll() => FlightTable.GetAllFlights( _dbConnection );

        [HttpGet( "{flightId}" )]
        public async Task<IActionResult> GetAsync( int flightId )
        {
            var res = await FlightTable.GetFlightById( _dbConnection, flightId );
            return res == null ? NotFound() : (IActionResult)Ok( res );
        }

        [HttpDelete( "{flightId}" )]
        public Task<bool> DeleteById( int flightId ) => FlightTable.DeleteFlightById( _dbConnection, flightId );

        [HttpPost]
        public Task<int> CreateAsync( [FromBody] FlightDTO flight )
            => FlightTable.CreateFlight( _dbConnection, flight.DepartureAirportId, flight.ArrivalAirportId, flight.TotalSeatCount );
    }
}
