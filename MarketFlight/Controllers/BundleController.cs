using System.Data;
using System.Threading.Tasks;
using MarketFlight.Data;
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

        [HttpPost]
        public Task<int> CreateAsync( double price )
            => BundleTable.CreateBundle( _dbConnection, price );

        [HttpPost( "{id}" )]
        public Task AddItem( int id, int flightId )
            => BundleTable.AddBundleItem( _dbConnection, id, flightId );
    }
}
