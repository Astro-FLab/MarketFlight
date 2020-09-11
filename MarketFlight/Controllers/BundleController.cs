using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MarketFlight.Data;
using MarketFlight.Model;
using Microsoft.AspNetCore.Mvc;

namespace MarketFlight.Controllers
{
    [ApiController]
    [Route( "marketflight/bundles" )]
    public class BundleController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public BundleController( IDbConnection dbConnection )
        {
            _dbConnection = dbConnection;
        }

        [HttpGet( "{bundleId}" )]
        public async Task<IActionResult> GetAsync( int bundleId )
        {
            var res = await BundleTable.GetBundleById( _dbConnection, bundleId );
            return res == null ? NotFound() : (IActionResult)Ok( res );
        }

        [HttpGet( "{bundleFlight}" )]
        public async Task<BundleModel> GetBundleItems( int bundleId )
        {
            FlightModel flight;
            BundleModel bundle = await BundleTable.GetBundleById( _dbConnection, bundleId );
            var flights_id = await BundleTable.GetBundleFlights( _dbConnection, bundleId );

            foreach( int flight_id in flights_id )
            {
                var k = await FlightTable.GetFlightById( _dbConnection, flight_id );
                if( k is null )
                {
                    throw new Exception();
                }
                else flight = k;
                bundle.Flights.Append( flight );
            }
            if( bundle is null )
            {
                throw new Exception();
            }
            else return bundle;
        }

        [HttpGet]
        public async Task<IEnumerable<BundleModel>> GetBundles()
        {
            IEnumerable<BundleModel> bundles = await BundleTable.GetAllBundle( _dbConnection ); 
            foreach(var bundle in bundles)
            {
                await GetBundleItems( bundle.BundleId );
            }
            return bundles;
        }

        [HttpPost]
        public Task<int> CreateAsync( double price )
            => BundleTable.CreateBundle( _dbConnection, price );

        [HttpPost( "{id}" )]
        public Task AddItem( int id, int flightId )
            => BundleTable.AddBundleItem( _dbConnection, id, flightId );
    }
}
