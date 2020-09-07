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
            => dbConnection.ExecuteAsync(
                @"insert into MF.tUser (FirstName, LastName)
                  output INSERTED.UserId
                  values(@FirstName, @LastName)", new { FirstName = firstName, LastName = lastName } );

        public static async Task<UserModel?> GetUserById( IDbConnection dbConnection, int userId )
            => (await dbConnection.QueryAsync<UserModel?>(
                           "select UserId, FirstName, LastName from MF.tUser where UserId = @UserId",
                           new { UserId = userId } )).FirstOrDefault();
    }
}
