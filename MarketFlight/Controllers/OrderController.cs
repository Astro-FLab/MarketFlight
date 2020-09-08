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
    [Route( "marketflight/orders" )]
    public class OrderController : ControllerBase
    {
        readonly IActivityMonitor _m;
        private readonly IDbConnection _dbConnection;

        public OrderController( IActivityMonitor m, IDbConnection dbConnection )
        {
            _m = m;
            _dbConnection = dbConnection;
        }
        [HttpGet]
        public Task<IEnumerable<OrderModel>> GetAll()
            => OrderTable.GetAllOrders( _dbConnection );

        [HttpGet( "marketflight/orders/{id}" )]
        public async Task<IActionResult> GetById( int id )
        {
            OrderModel? result = await OrderTable.GetOrderById( _dbConnection, id );
            if( result == null ) return NotFound();
            return Ok( result );
        }

        [HttpPost]
        public async Task<IActionResult> Create( int userId, int flightId, DateTime orderDate, int seatCount )
        {
            int res = await OrderTable.CreateOrder( _dbConnection, userId, flightId, orderDate, seatCount );
            if( res == -1 ) return BadRequest( "Not enough seats" );
            return Ok( res );
        }
    }
}
