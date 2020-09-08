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
        public static Task<int> CreateFlight( IDbConnection dbConnection, int departureAirportId, int arrivalAirportId )
            => dbConnection.QuerySingleAsync<int>(
                @"insert into MF.tFlight (DepartureAirport, ArrivalAirport)
                  output INSERTED.FlightId
                  values(@DepartureAirportId, @ArrivalAirportId)", new { DepartureAirportId = departureAirportId, ArrivalAirportId = arrivalAirportId } );

        public static async Task<FlightModel?> GetFlightById( IDbConnection dbConnection, int flightId )
            => (await dbConnection.QueryAsync<FlightModel?>(
                           @"select FlightId,
                             tf.DepartureAirport as 'DepartureAirportId', ta1.[Name] as 'DepartureAirportName',
                             tf.ArrivalAirport as 'ArrivalAirportId', ta2.[Name] as 'ArrivalAirportName'
                             from MF.tFlight tf
                             join MF.tAirport ta1 on tf.DepartureAirport = ta1.AirportId
                             join MF.tAirport ta2 on tf.ArrivalAirport = ta2.AirportId
                             where tf.FlightId = @FlightId",
                           new { FlightId = flightId } )).SingleOrDefault();

        public static Task<IEnumerable<FlightModel>> GetAllFlights( IDbConnection dbConnection )
            => dbConnection.QueryAsync<FlightModel>(
                           @"select FlightId,
                             tf.DepartureAirport as 'DepartureAirportId', ta1.[Name] as 'DepartureAirportName',
                             tf.ArrivalAirport as 'ArrivalAirportId', ta2.[Name] as 'ArrivalAirportName'
                             from MF.tFlight tf
                             join MF.tAirport ta1 on tf.DepartureAirport = ta1.AirportId
                             join MF.tAirport ta2 on tf.ArrivalAirport = ta2.AirportId
                             where tf.FlightId = @FlightId" );
        public static async Task<bool> DeleteFlightById( IDbConnection dbConnection, int flightId )
         => 1 == await dbConnection.ExecuteAsync( "delete from MF.tFlight where FlightId = @FlightId", new { FlightId = flightId } );
    }
}
