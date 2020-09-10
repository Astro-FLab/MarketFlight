using Dapper;
using MarketFlight.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MarketFlight.Data
{
    public static class UserTable
    {
        /// GET - Get all users
        public static Task<IEnumerable<UserModel>> GetAllUsers( IDbConnection dbConnection )
            => dbConnection.QueryAsync<UserModel>(
                           "select UserId, FirstName, LastName from MF.tUser" );

        // POST - Create a user (from :firstName(string), lastName(string))
        public static Task<int> CreateUser( IDbConnection dbConnection, string firstName, string lastName )
            => dbConnection.QuerySingleAsync<int>(
                @"insert into MF.tUser (FirstName, LastName)
                  output INSERTED.UserId
                  values(@FirstName, @LastName)", new { FirstName = firstName, LastName = lastName } );

        // GET - Get a user (by Id(int))
        public static async Task<UserModel?> GetUserById( IDbConnection dbConnection, int userId )
            => (await dbConnection.QueryAsync<UserModel?>(
                           "select UserId, FirstName, LastName from MF.tUser where UserId = @UserId",
                           new { UserId = userId } )).SingleOrDefault();

        ////IN COMING// PUT - Update a user (from: UserId(int), firstName(string), lastName(string))        
        public static Task<int> UpdateUserById (IDbConnection dbConnection,int userId, string firstName, string lastName)
        //    => dbConnection.QuerySingleAsync<int>(
        //        "update Mf.tUser SET FirstName = @firstName, LastName = @lastName where UserId = @userId",
        //        new {UserID = userId, FirstName = firstName, LastName = lastName });

        // GET - Get all orders for a user (from: UserID(int))
        public static Task<IEnumerable<OrderModel>> GetUserOrders( IDbConnection dbConnection, int userId )
            => dbConnection.QueryAsync<OrderModel>(
                "select * from MF.vOrder where UserId = @UserId", new { UserId = userId } );
    }
}
