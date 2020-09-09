using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CK.Core;
using MarketFlight.Data;
using MarketFlight.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MarketFlight.Controllers
{
    [ApiController]
    [Route( "marketflight/airport" )]
    public class AirportController : ControllerBase
    {
        readonly IActivityMonitor _m;
        private readonly IDbConnection _dbConnection;

        public AirportController( IActivityMonitor m, IDbConnection dbConnection )
        {
            _m = m;
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync( int airportId )
        {
            var res = await AirportTable.GetAirportById( _dbConnection, airportId );
            return res == null ? NotFound() : (IActionResult)Ok( res );
        }

        [HttpPost]
        public Task<int> CreateAsync( string name )
            => AirportTable.CreateAirport( _dbConnection, name );

        [HttpGet( "byName/{name}" )]
        public Task<IEnumerable<AirportModel>> FindByName( string name ) => AirportTable.FindAirportByName( _dbConnection, name );
    }
}
