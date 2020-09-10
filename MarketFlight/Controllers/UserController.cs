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
    [Route( "marketflight/user" )]
    public class UserController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public UserController( IActivityMonitor m, IDbConnection dbConnection )
        {
            _dbConnection = dbConnection;
        }

        [HttpGet( "{userId}" )]
        public async Task<IActionResult> GetAsync( int userId )
        {
            UserModel? res = await UserTable.GetUserById( _dbConnection, userId );
            return res == null ? NotFound() : (IActionResult)Ok( res );
        }

        [HttpGet( "{userId}/orders" )]
        public Task<IEnumerable<OrderModel>> GetUserOrder( int userId ) => UserTable.GetUserOrders( _dbConnection, userId );

        [HttpGet]
        public Task<IEnumerable<UserModel>> GetAllAsync() => UserTable.GetAllUsers( _dbConnection );

        [HttpPost]
        public Task<int> CreateAsync( string firstName, string lastName )
            => UserTable.CreateUser( _dbConnection, firstName, lastName );
    }
}
