using Dapper;
using MarketFlight.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlight.Data
{
    public static class AirportTable
    {
        public static Task<int> CreateAirport( IDbConnection dbConnection, string name )
            => dbConnection.QuerySingleAsync<int>(
                @"insert into MF.tAirport ([Name])
                  output INSERTED.AirportId
                  values(@Name)", new { Name = name } );

        public static async Task<AirportModel?> GetAirportById( IDbConnection dbConnection, int airportId )
            => (await dbConnection.QueryAsync<AirportModel?>(
                           "select AirportId, [Name] from MF.tAirport where AirportId = @AiportId",
                           new { AirportId = airportId } )).SingleOrDefault();

        public static Task<IEnumerable<AirportModel>> FindAirportByName( IDbConnection dbConnection, string name )
            => dbConnection.QueryAsync<AirportModel>(
                "select AirportId, [Name] from MF.tAirport where [Name] like concat('%', @AirportName, '%')", new { AirportName = name }
                );

        public static Task<IEnumerable<AirportModel>> GetAllAirport( IDbConnection dbConnection )
        => dbConnection.QueryAsync<AirportModel>(
            "select AirportId, [Name] from MF.tAirport"
            );
    }
}
