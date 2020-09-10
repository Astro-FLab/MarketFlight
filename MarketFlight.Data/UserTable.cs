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
        public static Task<int> CreateUser( IDbConnection dbConnection, string firstName, string lastName )
            => dbConnection.QuerySingleAsync<int>(
                @"insert into MF.tUser (FirstName, LastName)
                  output INSERTED.UserId
                  values(@FirstName, @LastName)", new { FirstName = firstName, LastName = lastName } );

        public static async Task<UserModel?> GetUserById( IDbConnection dbConnection, int userId )
            => (await dbConnection.QueryAsync<UserModel?>(
                           "select UserId, FirstName, LastName from MF.tUser where UserId = @UserId",
                           new { UserId = userId } )).SingleOrDefault();
        public static async Task<UserModel?> GetUserByName( IDbConnection dbConnection, string name )
            => (await dbConnection.QueryAsync<UserModel?>(
                           "select UserId, FirstName, LastName from MF.tUser where Name like concat('%', @Name, '%');",
                           new { Name = name } )).SingleOrDefault();
        public static Task<IEnumerable<UserModel>> GetAllUsers( IDbConnection dbConnection )
            => dbConnection.QueryAsync<UserModel>(
                           "select UserId, FirstName, LastName from MF.tUser" );

        public static Task<IEnumerable<OrderModel>> GetUserOrders( IDbConnection dbConnection, int userId )
            => dbConnection.QueryAsync<OrderModel>(
                "select * from MF.vOrder where to.UserId = @UserId", new { UserId = userId } );
    }
}
