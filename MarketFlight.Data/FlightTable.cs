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
    public static class FlightTable
    {
        public static Task<int> CreateFlight( IDbConnection dbConnection, int departureAirportId, int arrivalAirportId, int totalSeatCount )
            => dbConnection.QuerySingleAsync<int>(
                @"insert into MF.tFlight (DepartureAirportId, ArrivalAirportId, TotalSeatCount)
                  output INSERTED.FlightId
                  values(@DepartureAirportId, @ArrivalAirportId, @TotalSeatCount)", new { DepartureAirportId = departureAirportId, ArrivalAirportId = arrivalAirportId, TotalSeatCount = totalSeatCount } );

        public static async Task<FlightModel?> GetFlightById( IDbConnection dbConnection, int flightId )
            => (await dbConnection.QueryAsync<FlightModel?>(
                           @"select * from MF.vFlight where FlightId = @FlightId",
                           new { FlightId = flightId } )).SingleOrDefault();

        public static Task<IEnumerable<FlightModel>> GetAllFlights( IDbConnection dbConnection )
            => dbConnection.QueryAsync<FlightModel>( @"select * from MF.vFlight" );
        public static async Task<bool> DeleteFlightById( IDbConnection dbConnection, int flightId )
         => 1 == await dbConnection.ExecuteAsync( "delete from MF.tFlight where FlightId = @FlightId", new { FlightId = flightId } );
    }
}
