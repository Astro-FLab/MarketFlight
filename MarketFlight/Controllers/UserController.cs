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
    [Route( "user" )]
    public class UserController : ControllerBase
    {
        readonly IActivityMonitor _m;
        private readonly IDbConnection _dbConnection;

        public UserController( IActivityMonitor m, IDbConnection dbConnection )
        {
            _m = m;
            _dbConnection = dbConnection;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync( int userId )
        {
            UserModel? res = await UserTable.GetUserById( _dbConnection, userId );
            return res == null ? NotFound() : (IActionResult)Ok( res );
        }

        [HttpPost]
        public Task<int> CreateAsync( string firstName, string lastName )
            => UserTable.CreateUser( _dbConnection, firstName, lastName );
    }
}
